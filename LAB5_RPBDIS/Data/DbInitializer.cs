
using LAB5_RPBDIS.Data;
using LAB5_RPBDIS.Models;

namespace LAB5_RPBDIS
{
    public class DbInitializer
    {
        public int TrainTypeCount { get; set; } = 10; 

        public int TrainCount { get; set; } = 100; 
        public int EmployeeCount { get; set; } = 100; 
        public int StopCount { get; set; } = 100; 
        public int PositionCount { get; set; } = 100; 

        public int ScheduleCount { get; set; } = 500; 
        public int TrainStaffCount { get; set; } = 200;

        public DbInitializer()
        {
            TrainTypeCount = 10;
            TrainCount = 100;
            EmployeeCount = 100;
            StopCount = 100;
            PositionCount = 100;
            ScheduleCount = 500;
            TrainStaffCount = 200;
        }
        public DbInitializer(int trainTypeCount, int trainCount, int employeeCount, int stopCount, int positionCount, int scheduleCount, int trainStaffCount)
        {
            TrainTypeCount = trainTypeCount;
            TrainCount = trainCount;
            EmployeeCount = employeeCount;
            StopCount = stopCount;
            PositionCount = positionCount;
            ScheduleCount = scheduleCount;
            TrainStaffCount = trainStaffCount;
        }

        // Метод для инициализации базы данных
        public void InitializeDatabase(RailwayTrafficContext db)
        {
            db.Database.EnsureCreated();

            // Проверка занесены ли в бд какие-нибудь записи
            if (db.Trains.Any() || db.Stops.Any() || db.Employees.Any() || db.TrainTypes.Any() || db.Positions.Any())
            {
                Console.WriteLine("\n\nБД УЖЕ ИНИЦИАЛИЗИРОВАНА\n\n");
                PrintTableRecordsCount(db);
                PrintFirstRecords(db);
                return;   // База данных инициализирована, т.к. присутствует минимум 1 запись (связывающие таблицы не проверяются)
            }
            Console.WriteLine("\n\nИнициализация бд\n\n");
            // Вызываем методы для заполнения таблиц данными
            FillTrainTypes(db);
            FillPositions(db);
            FillTrains(db);
            FillStops(db);
            FillEmployees(db);
            FillSchedules(db);
            FillTrainStaffs(db);
            PrintTableRecordsCount(db);
            PrintFirstRecords(db);
        }

        private void PrintTableRecordsCount(RailwayTrafficContext db)
        {
            Console.WriteLine("\nTable Records Count:");

            Console.WriteLine($"Trains: {db.Trains.Count()}");
            Console.WriteLine($"Stops: {db.Stops.Count()}");
            Console.WriteLine($"Employees: {db.Employees.Count()}");
            Console.WriteLine($"TrainTypes: {db.TrainTypes.Count()}");
            Console.WriteLine($"Positions: {db.Positions.Count()}");
            Console.WriteLine($"Schedules: {db.Schedules.Count()}");
            Console.WriteLine($"TrainStaffs: {db.TrainStaffs.Count()}");
            Console.WriteLine();
        }

        private void PrintFirstRecords(RailwayTrafficContext db)
        {
            Console.WriteLine("\nПервые 5 записей:");

            Console.WriteLine("Trains:");
            PrintRecords(db.Trains.Take(5)); // Выводим первые 5 записей

            Console.WriteLine("\nStops:");
            PrintRecords(db.Stops.Take(5));

            Console.WriteLine("\nEmployees:");
            PrintRecords(db.Employees.Take(5));

            Console.WriteLine("\nTrainTypes:");
            PrintRecords(db.TrainTypes.Take(5));

            Console.WriteLine("\nPositions:");
            PrintRecords(db.Positions.Take(5));
            // Добавь аналогичные строки для других таблиц
        }

        private void PrintRecords<T>(IEnumerable<T> records)
        {
            foreach (var record in records)
            {
                Console.WriteLine(record);
            }
        }

        public void FillTrainTypes(RailwayTrafficContext db)
        {
            for (int i = 1; i <= TrainTypeCount; i++)
            {
                string typeName = $"type_{i}";
                TrainType trainType = new TrainType
                {
                    TypeName = typeName
                };
                db.TrainTypes.Add(trainType);
            }
            db.SaveChanges();
        }

        public void FillTrains(RailwayTrafficContext db)
        {
            // Получаем все доступные TrainType из базы данных
            var availableTrainTypes = db.TrainTypes.ToList();

            for (int i = 1; i <= TrainCount; i++)
            {
                string trainNumber = $"train_{i}";
                float distanceInKm = (float)(new Random().NextDouble() * 500);
                bool isBrandedTrain = (new Random().Next(2) == 0); // Генерируем случайное булево значение

                // Получаем случайный TrainType из доступных
                TrainType randomTrainType = availableTrainTypes[new Random().Next(availableTrainTypes.Count)];

                Train train = new Train
                {
                    TrainNumber = trainNumber,
                    DistanceInKm = distanceInKm,
                    IsBrandedTrain = isBrandedTrain,
                    TrainType = randomTrainType
                };
                db.Trains.Add(train);
            }
            db.SaveChanges();
        }

        public void FillStops(RailwayTrafficContext db)
        {
            for (int i = 1; i <= StopCount; i++)
            {
                string stopName = $"Stop_{i}";
                bool isRailwayStation = new Random().Next(2) == 0; // Пример: 50% шанс быть железнодорожной станцией
                bool hasWaitingRoom = new Random().Next(2) == 0; // Пример: 50% шанс иметь зал ожидания

                // Создаем объект Stop
                var stop = new Stop
                {
                    Name = stopName,
                    IsRailwayStation = isRailwayStation,
                    HasWaitingRoom = hasWaitingRoom
                };
                db.Stops.Add(stop);
            }
            db.SaveChanges();
        }

        public void FillPositions(RailwayTrafficContext db)
        {
            for (int i = 1; i <= PositionCount; i++)
            {
                string positionName = $"position_{i}";
                float salaryUsd = (float)(new Random().NextDouble() * 5000);

                // Создаем объект Position
                Position position = new Position
                {
                    PositionName = positionName,
                    SalaryUsd = salaryUsd
                };
                db.Positions.Add(position);
            }
            db.SaveChanges();
        }

        public void FillEmployees(RailwayTrafficContext db)
        {
            // Получаем все доступные Positions из базы данных
            var availablePositions = db.Positions.ToList();

            for (int i = 1; i <= EmployeeCount; i++)
            {
                string employeeName = $"employee_{i}";
                int age = new Random().Next(18, 60); // Пример: от 18 до 59 лет
                float workExperience = (float)(new Random().NextDouble() * 20); // Пример: от 0 до 20 лет

                // Получаем случайную Position из доступных
                Position randomPosition = availablePositions[new Random().Next(availablePositions.Count)];

                Employee employee = new Employee
                {
                    EmployeeName = employeeName,
                    age = age,
                    WorkExperience = workExperience,
                    Position = randomPosition
                };
                db.Employees.Add(employee);
            }
            db.SaveChanges();
        }

        public void FillSchedules(RailwayTrafficContext db)
        {
            // Получаем все доступные Trains из базы данных
            var availableTrains = db.Trains.ToList();
            // Получаем все доступные Stops из базы данных
            var availableStops = db.Stops.ToList();

            for (int i = 1; i <= ScheduleCount; i++)
            {
                int dayOfWeek = new Random().Next(1, 8); // Пример: от 1 (понедельник) до 7 (воскресенье)

                TimeSpan? arrivalTime = TimeSpan.FromHours(new Random().Next(0, 24)) +
                        TimeSpan.FromMinutes(new Random().Next(0, 60));

                TimeSpan? departureTime = TimeSpan.FromHours(new Random().Next(0, 24)) +
                                          TimeSpan.FromMinutes(new Random().Next(0, 60));

                // Получаем случайный Train из доступных
                Train randomTrain = availableTrains[new Random().Next(availableTrains.Count)];
                // Получаем случайный Stop из доступных
                Stop randomStop = availableStops[new Random().Next(availableStops.Count)];

                Schedule schedule = new Schedule
                {
                    DayOfWeek = dayOfWeek,
                    ArrivalTime = arrivalTime,
                    DepartureTime = departureTime,
                    Train = randomTrain,
                    Stop = randomStop
                };
                db.Schedules.Add(schedule);
            }
            db.SaveChanges();
        }

        public void FillTrainStaffs(RailwayTrafficContext db)
        {
            // Получаем все доступные Employees из базы данных
            var availableEmployees = db.Employees.ToList();
            // Получаем все доступные Trains из базы данных
            var availableTrains = db.Trains.ToList();

            for (int i = 1; i <= TrainStaffCount; i++)
            {
                int dayOfWeek = new Random().Next(1, 8); // Пример: от 1 (понедельник) до 7 (воскресенье)
                // Получаем случайный Employee из доступных
                Employee randomEmployee = availableEmployees[new Random().Next(availableEmployees.Count)];
                // Получаем случайный Train из доступных
                Train randomTrain = availableTrains[new Random().Next(availableTrains.Count)];

                TrainStaff trainStaff = new TrainStaff
                {
                    DayOfWeek = dayOfWeek,
                    Employee = randomEmployee,
                    Train = randomTrain
                };
                db.TrainStaffs.Add(trainStaff);
            }
            db.SaveChanges();
        }




    }

}
