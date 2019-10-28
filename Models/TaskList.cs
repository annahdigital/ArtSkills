using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ArtSkills.Models
{
    public class TaskList
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public int Completed { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public virtual List<Task> Tasks { get; set; }

        [NotMapped]
        public bool Status => Percentage >= 100;

        [NotMapped]
        public double Percentage
        {
            get
            {
                if (Tasks.Count != 0)
                    return 100*Completed / Tasks.Count;
                else return 0;
            }
        }

        public TaskList()
        {
            Tasks = new List<Task>();
        }

        public TaskList(String name, ApplicationUser creator, string description)
        {
            this.Name = name;
            this.ApplicationUser = creator;
            this.Description = description;
            this.StartDate = DateTime.Now;
        }

        public void AddTask(string taskName)
        {
            Tasks.Add(new Task(taskName, this));
        }

        public bool CompleteTask(int index)
        {
            Tasks[index].Status = true;
            Completed++;
            if (Percentage < 100)
            {
                return false;
            }
            else
            {
                EndTaskList();
                return true;
            }
        }

        public void EndTaskList()
        {
            EndDate = DateTime.Now;
        }
    }
}
