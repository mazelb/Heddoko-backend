using DAL;
using Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace ImportConsole
{
    class Excel
    {
        public static void ImportSensors(string filename)
        {
            UnitOfWork uow = new UnitOfWork(new HDContext());


            FileStream stream = File.Open(filename, FileMode.Open, FileAccess.Read);
            List<SensorSet> sets = new List<SensorSet>();
            List<Sensor> sensors = new List<Sensor>();
            List<Sensor> sensorValidate = new List<Sensor>();
            using (IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream))
            {
                DataSet dataSet = excelReader.AsDataSet();

                DataTable table = dataSet.Tables["Sensors"];
                foreach (DataRow row in table.Rows)
                {
                    string serial = row[0]?.ToString();
                    string location = row[1]?.ToString();
                    string status = row[2]?.ToString();
                    string sensorSet = row[3]?.ToString();
                    string bodyID = row[4]?.ToString();

                    if (string.IsNullOrEmpty(status)
                      || status.Equals("Status"))
                    {
                        continue;
                    }

                    sensorSet = ParseSensorSet(sensorSet);

                    SensorSet set = sets.FirstOrDefault(c => c.Label.Equals(sensorSet, StringComparison.OrdinalIgnoreCase));

                    if (set == null)
                    {
                        if (!string.IsNullOrEmpty(sensorSet))
                        {
                            set = new SensorSet()
                            {
                                Label = sensorSet,
                                QAStatus = SensorSetQAStatusType.None,
                                Status = EquipmentStatusType.Ready,
                                Sensors = new List<Sensor>()
                            };
                            sets.Add(set);
                        }
                    }

                    Sensor sensor = new Sensor()
                    {
                        Label = ParseSerial(serial),
                        Location = location,
                        Status = ParseStatus(status),
                        AnatomicalLocation = ParseBodyID(bodyID),
                        QAStatus = SensorQAStatusType.None,
                        Type = SensorType.NodIMU
                    };

                    if (!string.IsNullOrEmpty(sensorSet))
                    {
                        set.Sensors.Add(sensor);
                        sensorValidate.Add(sensor);
                    }
                    else
                    {
                        sensors.Add(sensor);
                        sensorValidate.Add(sensor);
                    }
                }
            }

            var querySensors = sensors.GroupBy(c => c.Label)
                     .Where(g => g.Count() > 1)
                     .Select(c => c.Key).ToList();
            querySensors.ForEach(c => Console.WriteLine(c));

            var querySets = sets.Where(c => c.Sensors.Count() > 9).ToList();
            querySets.ForEach(c => Console.WriteLine(c.Label));

            if (querySensors.Count() != 0)
            {
                Console.WriteLine("Duplicate sensors");
                Console.ReadKey();

                return;
            }

            if (querySets.Count() != 0)
            {
                Console.WriteLine("Set are not valid");
                Console.ReadKey();

                return;
            }
            Console.WriteLine("Importing...");

            foreach (var set in sets)
            {
                uow.SensorSetRepository.Add(set);
            }

            foreach (var sensor in sensors)
            {
                uow.SensorRepository.Add(sensor);
            }
            uow.Save();

            Console.WriteLine("Imported to database");
            Console.ReadKey();
        }

        private static string ParseSerial(string serial)
        {
            if (string.IsNullOrEmpty(serial))
            {
                return null;
            }

            return serial.Trim().Substring(serial.Length - 4, 4);
        }

        private static string ParseSensorSet(string sensorSet)
        {
            if (string.IsNullOrEmpty(sensorSet))
            {
                return null;
            }

            return sensorSet?.Trim().ToLower().Replace("imu set", "").Trim();
        }

        private static EquipmentStatusType ParseStatus(string status)
        {
            switch (status.Trim().ToLower())
            {
                case "in use":
                    return EquipmentStatusType.InUse;
                case "defective":
                    return EquipmentStatusType.Defective;
                case "production":
                    return EquipmentStatusType.InProduction;
                case "ready":
                default:
                    return EquipmentStatusType.Ready;
            }
        }

        private static AnatomicalLocationType? ParseBodyID(string body)
        {
            int? anatomic = body.ToNullableInt();
            return anatomic.HasValue ? (AnatomicalLocationType)anatomic.Value : (AnatomicalLocationType?)null;
        }
    }
}
