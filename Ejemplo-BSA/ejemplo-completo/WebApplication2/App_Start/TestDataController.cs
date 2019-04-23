using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.IO;

namespace WebApplication2.App_Start
{
    public class TestDataController
    {
        public static TestData getTestDataById(string id_operation)
        {
            TestData TestData = new TestData();
            List<TestData> testDataList = GetAllData();
            foreach (var test in testDataList)
            {
                if (test.id_operation == id_operation)
                {
                    TestData = test;
                    break;
                }
            }

            return TestData;
        }

        public static List<TestData> GetAllData()
        {
            // si no existe, lo crea 
            if (!System.IO.File.Exists(@Globals.file_dir))
            {
                TestDataController.create_data_file();
            }
            return JsonConvert.DeserializeObject<List<TestData>>(File.ReadAllText(@Globals.file_dir));
        }

        public static void create_data_file()
        {
            if (!System.IO.File.Exists(@Globals.file_path))
            {
                System.IO.Directory.CreateDirectory(@Globals.file_path);
            }

            List<TestData> test_list = new List<TestData>();
            JsonSerializer serializer = new JsonSerializer();
            using (StreamWriter sw = new StreamWriter(@Globals.file_dir))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, test_list);
            }
            return;
        }


        public static void saveData(TestData data)
        {
            List<TestData> test_list = GetAllData();
            TestData TestData = getTestDataById(data.id_operation);
           
            if (TestData.id_operation != null)
            { // update DATA 
                List<TestData> test_list_updated = new List<TestData>();
                foreach (var test in test_list)
                {
                 
                    if (test.id_operation == data.id_operation)
                    {
                        test_list_updated.Add(data);
                    }
                    else
                    {
                        test_list_updated.Add(test);
                    }
                }

                test_list = test_list_updated;
            }
            else
            {  // INSERT NEW DATA
                System.Diagnostics.Debug.WriteLine("****** entre al else  ********");

                test_list.Add(data);
            }

            JsonSerializer serializer = new JsonSerializer();
            using (StreamWriter sw = new StreamWriter(@Globals.file_dir))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, test_list);
            }

        }


        public static void delete(string id_operation)
        {
            List<TestData> test_list = GetAllData();

            if (id_operation != null)
            {
                List<TestData> test_list_updated = new List<TestData>();
                foreach (var test in test_list)
                {                
                    if (test.id_operation != id_operation)
                    {
                        test_list_updated.Add(test);
                    }                 
                }
                test_list = test_list_updated;
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("****** no se encontro ningun registro para eliminar  ********");
            }

            JsonSerializer serializer = new JsonSerializer();
            using (StreamWriter sw = new StreamWriter(@Globals.file_dir))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, test_list);
            }
        }



    }
}