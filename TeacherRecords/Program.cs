using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TeacherRecords
{
    class Teacher
    {

        public int id { get; set; }
        public string name { get; set; }
        public string cla { get; set; }
        public string section { get; set; }

        public static string teacherFile = @"C:\Users\pvsriso\Documents\FSD\TeacherFile.txt";

        public static string getTeacherRecordbById(int teacherid)
        {
            string line = null;
            int id;

            if (!File.Exists(teacherFile))
                return null;

            System.IO.StreamReader file = new System.IO.StreamReader(teacherFile);
            while ((line = file.ReadLine()) != null)
            {
                id = Convert.ToInt32(line.Split(',')[0]);
                if (id == teacherid)
                    break;
            }
            file.Close();
            return line;
        }

        public static void showTeacherRecordById(int teacherid)
        {

            string teacherRecord = getTeacherRecordbById(teacherid);
            if (teacherRecord != null)
            {
                string[] teacher = teacherRecord.Split(',');
                Console.WriteLine("Teacher id: " + teacher[0]);
                Console.WriteLine("Teacher Name: " + teacher[1]);
                Console.WriteLine("Teacher Class: " + teacher[2]);
                Console.WriteLine("Teacher Section: " + teacher[3]);
            }
            else
            {
                Console.WriteLine("No record found with id " + teacherid);
            }
        }

        public void AddTeacherRecord()
        {
            int tempid;
            Console.WriteLine("Enter Teacher ID ");
            try
            {
                tempid = Convert.ToInt32(Console.ReadLine());

                if (getTeacherRecordbById(tempid) == null)
                {
                    this.id = tempid;
                }
                else
                {
                    Console.WriteLine("Teacher record exist with this id");
                    return;
                }

                Console.WriteLine("Enter Teacher Name ");
                this.name = Console.ReadLine();

                Console.WriteLine("Enter Teacher cla ");
                this.cla = Console.ReadLine();

                Console.WriteLine("Enter Teacher section ");
                this.section = Console.ReadLine();

                this.AddRecordInFile();
            }
            catch
            {
                Console.WriteLine("Invalid input");
            }
            
            

        }

        public override string ToString()
        {
            string teacherRecord = this.id.ToString().Trim() + "," + this.name.Trim() + "," + this.cla.Trim() + "," + this.section.Trim() + Environment.NewLine;
            return teacherRecord;
        }

        public void AddRecordInFile()
        {
            File.AppendAllText(teacherFile, this.ToString());
        }



        public static void updateTeacherRecordById(int teacherid)
        {
            string teacherRecord = getTeacherRecordbById(teacherid);
            if (teacherRecord == null)
            {
                Console.WriteLine("No teacher record found with id: " + teacherid);
                return;
            }
            string[] teacher = teacherRecord.Trim().Split(',');
            Console.WriteLine("Teacher Details:\nID: " + teacher[0] + "\nName: " + teacher[1] +
                "\nClass: " + teacher[2] + "\nSection: " + teacher[3]);


            Teacher newteacher = new Teacher();
            newteacher.id = Convert.ToInt32(teacher[0]);

            Console.Write("Enter new Name(Press Enter if don't want to update):");
            newteacher.name = Console.ReadLine();
            if (String.IsNullOrEmpty(newteacher.name))
            {
                newteacher.name = teacher[1];
            }

            Console.Write("Enter new Class(Press Enter if don't want to update):");
            newteacher.cla = Console.ReadLine();
            if (String.IsNullOrEmpty(newteacher.cla))
            {
                newteacher.cla = teacher[2];
            }

            Console.Write("Enter new Section(Press Enter if don't want to update):");
            newteacher.section = Console.ReadLine();
            if (String.IsNullOrEmpty(newteacher.section))
            {
                newteacher.section = teacher[3];
            }

            string text = File.ReadAllText(teacherFile);
            text = text.Replace(teacherRecord, newteacher.ToString().Trim());
            File.WriteAllText(teacherFile, text);

        }

        public static void deleteTeacherRecordById(int teacherid)
        {
            string teacherRecord = getTeacherRecordbById(teacherid);
            if (teacherRecord == null)
            {
                Console.WriteLine("No teacher record found with id: " + teacherid);
                return;
            }

            string text = File.ReadAllText(teacherFile);
            text = text.Replace(teacherRecord + "\r\n", "");
            File.WriteAllText(teacherFile, text);
        }

        public static void showAllTeacherRecords()
        {
            string line = null;
            int count = 0;

            if (!File.Exists(teacherFile))
                return;

            System.IO.StreamReader file = new System.IO.StreamReader(teacherFile);
            while ((line = file.ReadLine()) != null)
            {
                count++;
                Console.WriteLine("\nTeacher Record : " + count);
                string[] teacher = line.Trim().Split(',');
                Console.WriteLine("\nID: " + teacher[0] + "\nName: " + teacher[1] +
                    "\nClass: " + teacher[2] + "\nSection: " + teacher[3]);

            }
            file.Close();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {

            int wrongInputCounter = 0;
            int choice;
            int teacherid;

            while (true)
            {
                Console.WriteLine("===========================================");
                Console.WriteLine("1.Add Teacher Record");
                Console.WriteLine("2.Show Teacher Record by Id");
                Console.WriteLine("3. Update Teacher record by Id");
                Console.WriteLine("4. Delete Teacher Record by Id");
                Console.WriteLine("5. Show all teacher Records");
                Console.WriteLine("6.Exit");
                Console.WriteLine("===========================================");
                Console.Write("Input your choice: ");
                try
                {
                    choice = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Invalid input");
                    continue;
                }
                switch (choice)
                {
                    case 1:
                        Teacher t1 = new Teacher();
                        t1.AddTeacherRecord();
                        break;
                    case 2:
                        Console.WriteLine("Enter teacher id to display:");
                        teacherid = Convert.ToInt32(Console.ReadLine());
                        Teacher.showTeacherRecordById(teacherid);
                        break;
                    case 3:
                        Console.WriteLine("Enter teacher id to update: ");
                        teacherid = Convert.ToInt32(Console.ReadLine());
                        Teacher.updateTeacherRecordById(teacherid);
                        break;
                    case 4:
                        Console.WriteLine("Enter teacher id to delete: ");
                        teacherid = Convert.ToInt32(Console.ReadLine());
                        Teacher.deleteTeacherRecordById(teacherid);
                        break;
                    case 5:
                        Teacher.showAllTeacherRecords();
                        break;
                    case 6:
                        Environment.Exit(0);
                        break;
                    default:
                        wrongInputCounter++;
                        Console.WriteLine("Invalid input. 2 more chances to try");
                        if (wrongInputCounter > 3)
                        {
                            Console.WriteLine("Maxmimum invalid inputs");
                            Environment.Exit(0);
                        }
                        break;
                }

            }


        }
    }
}