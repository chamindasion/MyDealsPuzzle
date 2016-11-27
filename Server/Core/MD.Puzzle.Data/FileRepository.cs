
using MD.Puzzle.Data.Contracts;
using MD.Puzzle.Models.DomainObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MD.Puzzle.Data
{
    public class FileRepository : IFileRepository
    {
        private readonly string _fileSourcePath = String.Format(@"{0}\{1}",
            Convert.ToString(AppDomain.CurrentDomain.GetData("DataDirectory")), "TextFileDataBase.txt");

        private readonly List<RecordLocator> _recordLocators;

        public FileRepository()
        {
            var passengers = new List<Passenger>();
            _recordLocators = new List<RecordLocator>();

            using (var streamReader = new StreamReader(_fileSourcePath, Encoding.UTF8))
            {
                var readContents = streamReader.ReadToEnd();
                if (!String.IsNullOrWhiteSpace(readContents))
                {
                    var result =
                        readContents.Split(new[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries)
                            .Where(p => p.StartsWith("1"));
                    foreach (var item in result)
                    {
                        var values = item.Split('/');
                        if (values.Length == 3)
                        {
                            var firtsName = values[0].Substring(1);
                            var lastName = values[1].Substring(0,
                                values[1].Length - (values[1].Length - values[1].IndexOf('.')));
                            var recordLocatorName = values[2];
                            RecordLocator tempRecordLocator = null;
                            if (!string.IsNullOrWhiteSpace(recordLocatorName))
                            {
                                tempRecordLocator = _recordLocators.FirstOrDefault(p => p.Name.Equals(recordLocatorName));
                                if (tempRecordLocator == null)
                                {
                                    tempRecordLocator = new RecordLocator() { Id = Guid.NewGuid(), Name = values[2] };
                                    _recordLocators.Add(tempRecordLocator);
                                }
                            }
                            if (tempRecordLocator != null && !string.IsNullOrWhiteSpace(firtsName) &&
                                !string.IsNullOrWhiteSpace(lastName))
                            {
                                var tempPassenger = passengers.FirstOrDefault(p => p.FirstName.Equals(firtsName) &&
                                                                                   p.LastName.Equals(lastName));
                                if (tempPassenger == null)
                                {
                                    tempPassenger = new Passenger()
                                    {
                                        Id = Guid.NewGuid(),
                                        FirstName = firtsName,
                                        LastName = lastName,
                                        RecordLocator = tempRecordLocator
                                    };
                                    passengers.Add(tempPassenger);
                                }
                            }

                            foreach (var locator in _recordLocators)
                                locator.Passengers = passengers.Where(p => p.RecordLocator.Id == locator.Id).ToList();
                        }
                    }
                }
            }
        }

        public IEnumerable<RecordLocator> Get(string searchCriteria)
        {
            if (string.IsNullOrWhiteSpace(searchCriteria))
            {
                return _recordLocators;
            }
            var regEx = new System.Text.RegularExpressions.Regex(searchCriteria);
            return _recordLocators.Where(p => regEx.IsMatch(p.Name.ToLower())
                || p.Passengers.Any(q => regEx.IsMatch(q.FirstName.ToLower()))
                || p.Passengers.Any(q => regEx.IsMatch(q.LastName.ToLower())));
        }

        public void Add(Passenger passenger, string recordLocatorName)
        {
            if (passenger != null)
            {
                using (var file = new StreamWriter(_fileSourcePath, true))
                {
                    file.WriteLine("1{0}/{1}.L/{2}", passenger.FirstName, passenger.LastName, recordLocatorName);
                }
            }
        }
    }
}