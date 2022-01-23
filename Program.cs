using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace prjToDoList
{
    class Program
    {
        static void Main(string[] args)
        {
            List<clsTask> lTasks = new List<clsTask>();
            ReadTasksFromFile(lTasks);
            int iSelection;
            do
            {
                Console.WriteLine("1- Add New Task");
                Console.WriteLine("2- Show finished tasks");
                Console.WriteLine("3- Show unfinished tasks");
                Console.WriteLine("4- Sort the list by due date");
                Console.WriteLine("5- Sort the list by priority");
                Console.WriteLine("6- Mark task as finished");
                Console.WriteLine("7- Save list to file");
                Console.WriteLine("8- Exit!");
                iSelection = int.Parse(Console.ReadLine());
                Console.Clear();
                switch (iSelection)
                {
                    case 1:
                        AddNewTask(lTasks);
                        break;
                    case 2:
                        ShowTaskList(lTasks, iSelection);
                        break;
                    case 3:
                        ShowTaskList(lTasks, iSelection);
                        break;
                    case 4:
                        ShowTaskList(lTasks, iSelection);
                        break;
                    case 5:
                        ShowTaskList(lTasks, iSelection);
                        break;
                    case 6:
                        MarkTaskAsFinished(lTasks);
                        break;
                    case 7:
                        SaveList(lTasks);
                        break;
                }
            } while (iSelection != 8);
            Console.WriteLine("Finished! click to any button to close.");
            Console.ReadKey();
        }
        public static void ReadTasksFromFile(List<clsTask> lTasks)
        {
            FileStream fs = new FileStream("listOfTask.txt", FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            string[] sData;
            string sText;
            DateTime sDueDate;
            bool sIsPriority, sIsFinished;
            if ("listOfTask.txt".Length != 0)
            {
                while (!sr.EndOfStream)
                {
                    sData = sr.ReadLine().Split(':');
                    if (sData.Length == 4)
                    {
                        sData[0] = sData[0].Substring(2, 15);
                        sIsPriority = bool.Parse(sData[0].ToString().Trim());
                        sIsFinished = bool.Parse(sData[1].ToString().Trim());
                        sDueDate = DateTime.Parse(sData[2].ToString().Trim());
                        sText = sData[3].Trim();
                        clsTask task = new clsTask(sText, sDueDate, sIsPriority, sIsFinished);
                        lTasks.Add(task);
                    }
                }
            }
            sr.Close();
            fs.Close();
        }
        public static void AddNewTask(List<clsTask> lTasks)
        {
            string sText;
            DateTime dtDueDate;
            bool bIsPriority = false;
            Console.WriteLine("Enter the text of task: ");
            sText = Console.ReadLine();

            Console.WriteLine("Enter the due date of task(ex. 22/11/1963): ");
            dtDueDate = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Do you want priority for this task (yes or no): ");
            if (Console.ReadLine() == "yes")
            {
                bIsPriority = true;
            }

            clsTask task = new clsTask(sText, dtDueDate, bIsPriority, false);
            lTasks.Add(task);

            Console.WriteLine("task added!");
        }
        public static void ShowTaskList(List<clsTask> lTasks, int iSelection)
        {
            List<clsTask> lTempTasks = new List<clsTask>();
            int iOrdinal = 1;
            switch (iSelection)
            {
                case 2:
                    foreach (clsTask task in lTasks)
                    {
                        if (task.bIsFinished == true)
                        {
                            lTempTasks.Add(task);
                        }
                    }
                    break;
                case 3:
                    foreach (clsTask task in lTasks)
                    {
                        if (task.bIsFinished == false)
                        {
                            lTempTasks.Add(task);
                        }
                    }
                    break;
                case 4:
                    lTempTasks = CompareTasksDueDate(lTasks);
                    break;
                case 5:
                    foreach (clsTask task in lTasks)
                    {
                        if (task.bIsPriority == true)
                        {
                            lTempTasks.Add(task);
                        }
                    }
                    break;
            }
            Console.WriteLine("-------------------------------------------------------------------------------");
            Console.WriteLine(string.Format("  {0,-15} | {1,-20} | {2,-50}", "Priority", "Due Date", "Text"));
            foreach (clsTask task in lTempTasks)
            {

                Console.WriteLine(string.Format(iOrdinal + " {0,-15} : {1,-20} : {2,-40}", task.bIsPriority, (task.dtDueDate.ToString()).Substring(0, 10), task.sText));
                iOrdinal++;
            }
            Console.WriteLine("-------------------------------------------------------------------------------");
        }
        public static List<clsTask> CompareTasksDueDate(List<clsTask> lTasks)
        {
            var lTempTasks1 = from task in lTasks//görevler zamana göre sıralanıp lTempTasks1 'e atanıyor
                              orderby task.dtDueDate
                              select task;

            return lTempTasks1.ToList();
        }
        public static void MarkTaskAsFinished(List<clsTask> lTasks)
        {
            int iSelection;
            List<clsTask> lTempTasks = new List<clsTask>();
            foreach (clsTask task in lTasks)
            {
                if (task.bIsFinished == false)
                {
                    lTempTasks.Add(task);
                }
            }
            ShowTaskList(lTasks, 3);
            Console.WriteLine("which one do you want to mark as finished ?");
            iSelection = int.Parse(Console.ReadLine()) - 1;
            lTempTasks[iSelection].bIsFinished = true;
            Console.WriteLine("Task is finished! congratulations\n");
        }
        public static void SaveList(List<clsTask> lTasks)
        {
            FileStream fs = new FileStream("listOfTask.txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            int iOrdinal = 1;
            sw.WriteLine("  {0,-15} | {1,-20} | {2,-20} | {3,-50}", "Priority", "Finished", "Due Date", "Text");
            foreach (clsTask task in lTasks)
            {
                sw.WriteLine(iOrdinal + " {0,-15} : {1,-20} : {2,-20} : {3,-40}", task.bIsPriority, task.bIsFinished, (task.dtDueDate.ToString()).Substring(0, 10), task.sText);
                iOrdinal++;
            }
            sw.Close();
            fs.Close();
            Console.WriteLine("-------------------------------------------------------------------------------");
            Console.WriteLine("List is saved to file.");
            Console.WriteLine("-------------------------------------------------------------------------------");
        }
    }
}