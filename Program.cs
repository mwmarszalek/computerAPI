using System;
using System.Data;
using System.Globalization;
using System.Text.Json;
using System.Text.RegularExpressions;
using AutoMapper;
using Dapper;
using HelloWorld.Data;
using HelloWorld.Models;
using HelloWorld.Models.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace HelloWorld
{
    internal class Program
    {
        static void Main(string[] args)
        {

            IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

            DataContextDapper dapper = new DataContextDapper(config);

            // Console.WriteLine(rightNow.ToString());

            // Computer computer = new Computer() 
            // {
            //     Motherboard = "Z690",
            //     HasWifi = true,
            //     HasLTE = false,
            //     ReleaseDate = DateTime.Now,
            //     Price = 943.87m,
            //     VideoCard = "RTX 2060"
            // };


            // string sql = @"INSERT INTO TutorialAppSchema.Computer (
            //     Motherboard,
            //     HasWifi,
            //     HasLTE,
            //     ReleaseDate,
            //     Price,
            //     VideoCard
            // ) VALUES ('" + computer.Motherboard 
            //         + "','" + computer.HasWifi
            //         + "','" + computer.HasLTE
            //         + "','" + computer.ReleaseDate.ToString("yyyy-MM-dd")
            //         + "','" + computer.Price.ToString("0.00", CultureInfo.InvariantCulture)
            //         + "','" + computer.VideoCard
            // + "')";

            // File.WriteAllText("log.txt", "\n" + sql + "\n");

            // using StreamWriter openFile = new("log.txt", append: true);

            // openFile.WriteLine("\n" + sql + "\n");

            // openFile.Close();

            string computersJson = File.ReadAllText("ComputersSnake.json");

            Mapper mapper = new Mapper(new MapperConfiguration((cfg) =>
            {
                cfg.CreateMap<ComputerSnake, Computer>()
                .ForMember(destination => destination.ComputerId, options =>
                options.MapFrom(source => source.computer_id))
                .ForMember(destination => destination.CPUCores, options =>
                options.MapFrom(source => source.cpu_cores))
                .ForMember(destination => destination.HasLTE, options =>
                options.MapFrom(source => source.has_lte))
                .ForMember(destination => destination.HasWifi, options =>
                options.MapFrom(source => source.has_wifi))
                .ForMember(destination => destination.Motherboard, options =>
                options.MapFrom(source => source.motherboard))
                .ForMember(destination => destination.ReleaseDate, options =>
                options.MapFrom(source => source.release_date))
                .ForMember(destination => destination.Price, options =>
                options.MapFrom(source => source.price));
            }));

            IEnumerable<ComputerSnake>? computersSystem = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<ComputerSnake>>(computersJson);


            IEnumerable<Computer>? computersSystemJsonPropertyMapping = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Computer>>(computersJson);

            if (computersSystemJsonPropertyMapping != null)
            {

                foreach (Computer computer in computersSystemJsonPropertyMapping)
                {
                    Console.WriteLine(computer.Motherboard);
                }
            }



            // Console.WriteLine(computersJson);

            //     JsonSerializerOptions options = new JsonSerializerOptions()
            //     {
            //         PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            //     };

            //     IEnumerable<Computer>? computersNewtonsoft = JsonConvert.DeserializeObject<IEnumerable<Computer>>(computersJson);

            //     if (computersNewtonsoft != null)
            //     {
            //         foreach (Computer computer in computersNewtonsoft)
            //         {
            //             string sql = @"INSERT INTO TutorialAppSchema.Computer (
            //         Motherboard,
            //         HasWifi,
            //         HasLTE,
            //         ReleaseDate,
            //         Price,
            //         VideoCard
            //     ) VALUES ('" + EscapeSingleQuote(computer.Motherboard)
            //            + "','" + computer.HasWifi
            //            + "','" + computer.HasLTE
            //            + "','" + computer.ReleaseDate
            //            + "','" + computer.Price
            //            + "','" + EscapeSingleQuote(computer.VideoCard)
            //    + "')";

            //         dapper.ExecuteSql(sql);
            //         }
            //     }


            //     JsonSerializerSettings settings = new JsonSerializerSettings()
            //     {
            //         ContractResolver = new CamelCasePropertyNamesContractResolver()
            //     };

            //     string computersCopyNewtonsoft = JsonConvert.SerializeObject(computersNewtonsoft, settings);

            //     File.WriteAllText("computersCopyNewtonsoft.txt", computersCopyNewtonsoft);

            //     string computersCopySystem = System.Text.Json.JsonSerializer.Serialize(computersSystem, options);

            //     File.WriteAllText("computersCopySystem.txt", computersCopySystem);

        }

        static string EscapeSingleQuote(string input)
        {
            string output = input.Replace("'", "''");

            return output;
        }

    }
}