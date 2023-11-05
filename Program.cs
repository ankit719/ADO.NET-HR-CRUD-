using Microsoft.Extensions.Configuration;

namespace HRDemo
{
    internal class Program
    {
        
        private static IConfiguration _iconfiguration;
        static void Main(string[] args)
        {
            GetAppSettingsFile();
            EmpDisplay();
        }
        static void GetAppSettingsFile()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("Appsettings.json", optional: false, reloadOnChange: true);
            _iconfiguration = builder.Build();
        }
        static void EmpDisplay()
        {
            Strongly_type indata = new Strongly_type(_iconfiguration);
            List<Employee> ls = indata.GetList();
            foreach (var x in ls)
                Console.WriteLine("{0}  {1}  {2}  {3}  {4}", x.Id, x.Name, x.Salary,x.Salary,x.Address);

            Console.WriteLine("------------------------------------------------------------");
           // int no = indata.Del(3);
           //Console.WriteLine("deleted {0}", no);
            Console.WriteLine("------------------------------------------------------------");
            List<Employee> ls1 = indata.GetList();
            foreach (var x in ls1)
                Console.WriteLine("{0}  {1}  {2}  {3}  {4}", x.Id, x.Name, x.Salary, x.Salary, x.Address);
            Console.WriteLine("------------------------------------------------------------");
            Employee p1 = new Employee {Name = "Raviraj", Salary = "90000" ,Gender="Male" ,Address="lmc" };
            //int a = indata.AddData(p1);
           // Console.WriteLine("{0}", a);
            Console.WriteLine("------------------------------------------------------------");
            List<Employee> ls2 = indata.GetList();
            foreach (var x in ls2)
                Console.WriteLine("{0}  {1}  {2}  {3}  {4}", x.Id, x.Name, x.Salary, x.Salary, x.Address);
            Console.WriteLine("------------------------------------------------------------");
            Employee r = indata.search(1);
            Console.WriteLine("{0} {1} {2} {3} {4}", r.Id, r.Name, r.Salary,r.Gender,r.Address); 



        }
    }

   
}