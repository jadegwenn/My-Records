using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace TeachersDigitalClassRecord;

// Jade Panilagan BSCpE - 2


public abstract class BaseMenu
{
    public QuarterScore QuarterScore
    {
        get => default;
        set
        {
        }
    }

    public abstract void DisplayMainMenu();
    public abstract void DisplayWelcome();
}


// ScoreMenu class inherits from BaseMenu
public class ScoreMenu : BaseMenu
{
    public override void DisplayWelcome()
    {
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(@"

                               ███╗   ███╗██╗   ██╗    ██████╗ ███████╗ ██████╗ ██████╗ ██████╗ ██████╗ ███████╗
                               ████╗ ████║╚██╗ ██╔╝    ██╔══██╗██╔════╝██╔════╝██╔═══██╗██╔══██╗██╔══██╗██╔════╝
                               ██╔████╔██║ ╚████╔╝     ██████╔╝█████╗  ██║     ██║   ██║██████╔╝██║  ██║███████╗
                               ██║╚██╔╝██║  ╚██╔╝      ██╔══██╗██╔══╝  ██║     ██║   ██║██╔══██╗██║  ██║╚════██║
                               ██║ ╚═╝ ██║   ██║       ██║  ██║███████╗╚██████╗╚██████╔╝██║  ██║██████╔╝███████║
                               ╚═╝     ╚═╝   ╚═╝       ╚═╝  ╚═╝╚══════╝ ╚═════╝ ╚═════╝ ╚═╝  ╚═╝╚═════╝ ╚══════╝
                                                                                                                      
                                                        
                ");

        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine(); 
        Console.WriteLine();
        Console.WriteLine(); 

        Console.ResetColor();
        Console.ForegroundColor= ConsoleColor.White;
        string prompt = "               Press any key to continue...";
        int windowWidth = Console.WindowWidth;
        int promptLength = prompt.Length;

        // Calculate the starting position for the prompt
        int startPosition = (windowWidth / 2) - (promptLength / 2);

        // Ensure the start position is valid (not negative)
        startPosition = Math.Max(startPosition, 0);

        // Set cursor position to startPosition and write the prompt
        Console.SetCursorPosition(startPosition, Console.CursorTop);
        Console.Write(prompt);

        // Wait for user to press a key
        Console.ReadKey();
    }

    public override void DisplayMainMenu()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.White;

        Console.WriteLine();
        string[] menuOptions = new string[]
        {
    "Add Student",
    "Update Student Record",
    "Delete Student Record",
    "Search Student Records",
    "Exit"
        };

        int windowWidth = Console.WindowWidth;
        int menuWidth = 40; // Define the width of each option box
        int startPosition = (windowWidth / 2) - (menuWidth / 2);

        // Draw the main menu title
        Console.SetCursorPosition(startPosition, Console.CursorTop);
        Console.WriteLine(" === Teacher's Digital Class Record ===");
        Console.WriteLine();

        // Draw each option in a closed-line table
        for (int i = 0; i < menuOptions.Length; i++)
        {
            // Draw the top border of the option box
            Console.SetCursorPosition(startPosition, Console.CursorTop);
            Console.WriteLine("┌" + new string('─', menuWidth - 2) + "┐");

            // Center the option with numbering within the defined menu width
            string option = $"{i + 1}. {menuOptions[i]}";
            int optionLength = option.Length;
            int padding = (menuWidth - 2 - optionLength) / 2; // Calculate padding for centering
            string centeredOption = "│" + new string(' ', padding) + option + new string(' ', menuWidth - 2 - optionLength - padding) + "│";

            Console.SetCursorPosition(startPosition, Console.CursorTop);
            Console.WriteLine(centeredOption);

            // Draw the bottom border of the option box
            Console.SetCursorPosition(startPosition, Console.CursorTop);
            Console.WriteLine("└" + new string('─', menuWidth - 2) + "┘");
        }

        Console.ResetColor(); // Reset color to default if needed
    }

}

// Assessment class to store different types of assessments
public class Assessment
{
    public string Type { get; set; }
    public int Score { get; set; }
    public int MaxScore { get; set; }

    public QuarterScore QuarterScore
    {
        get => default;
        set
        {
        }
    }

    public string GetPassFailStatus()
    {
        double percentage = (double)Score / MaxScore * 100;
        return percentage >= 75 ? "Passed" : "Failed";
    }

    public bool IsPassed()
    {
        return (Score / (double)MaxScore) * 100 >= 75;
    }

    public string GetResult()
    {
        return Score < MaxScore ? "Failed" : "Passed";
    }

    public override string ToString()
    {
        return $"{Type}: {Score}/{MaxScore} - {GetResult()}";
    }
}

// QuarterScore class
public class QuarterScore
{
    public int Quarter { get; set; }
    public List<Assessment> Assessments { get; set; } = new List<Assessment>();

    public double ComputeQuarterAverage()
    {
        if (Assessments.Count == 0)
            return 0;

        double totalScore = 0;
        double totalMaxScore = 0;

        foreach (var assessment in Assessments)
        {
            totalScore += assessment.Score;
            totalMaxScore += assessment.MaxScore;
        }

        return totalMaxScore > 0 ? (totalScore / totalMaxScore) * 100 : 0;
    }

    public double ComputeWeightedQuarterScore(string subject)
    {
        if (Assessments.Count == 0) return 0;

        // Define weights based on the subject
        double writtenWorksWeight = 0;
        double performanceTasksWeight = 0;
        double quarterlyAssessmentWeight = 0;

        // Set weights based on the subject
        switch (subject)
        {
            case "English":
            case "AP":
            case "ESP":
                writtenWorksWeight = 0.30;
                performanceTasksWeight = 0.50;
                quarterlyAssessmentWeight = 0.20;
                break;
            case "Science":
            case "Math":
                writtenWorksWeight = 0.40;
                performanceTasksWeight = 0.40;
                quarterlyAssessmentWeight = 0.20;
                break;
            case "MAPEH":
            case "TLE":
                writtenWorksWeight = 0.20;
                performanceTasksWeight = 0.60;
                quarterlyAssessmentWeight = 0.20;
                break;
        }

        // Initialize scores and max scores
        double writtenWorksScore = 0, writtenWorksMax = 0;
        double performanceTasksScore = 0, performanceTasksMax = 0;
        double quarterlyAssessmentScore = 0, quarterlyAssessmentMax = 0;

        foreach (var assessment in Assessments)
        {
            if (assessment.Type == "Written Work")
            {
                writtenWorksScore += assessment.Score;
                writtenWorksMax += assessment.MaxScore;
            }
            else if (assessment.Type == "Performance Task")
            {
                performanceTasksScore += assessment.Score;
                performanceTasksMax += assessment.MaxScore;
            }
            else if (assessment.Type == "Quarterly Assessment")
            {
                quarterlyAssessmentScore += assessment.Score;
                quarterlyAssessmentMax += assessment.MaxScore;
            }
        }

        // Calculate weighted scores for each component
        double weightedWrittenWorks = (writtenWorksMax > 0 ? (writtenWorksScore / writtenWorksMax) : 0) * writtenWorksWeight;
        double weightedPerformanceTasks = (performanceTasksMax > 0 ? (performanceTasksScore / performanceTasksMax) : 0) * performanceTasksWeight;
        double weightedQuarterlyAssessment = (quarterlyAssessmentMax > 0 ? (quarterlyAssessmentScore / quarterlyAssessmentMax) : 0) * quarterlyAssessmentWeight;

        // Sum the weighted scores
        return (weightedWrittenWorks + weightedPerformanceTasks + weightedQuarterlyAssessment) * 100;
    }

    public class SubjectScore
    {
        public string Subject { get; set; }
        public List<QuarterScore> Quarters { get; set; } = new List<QuarterScore>();

        public double ComputeWeightedScore()
        {
            double totalScore = 0;
            double totalMaxScore = 0;

            foreach (var quarter in Quarters)
            {
                double quarterAverage = quarter.ComputeQuarterAverage();
                totalScore += quarterAverage; // Total weighted score for the subject
                totalMaxScore += 100; // Each quarter is out of 100 (percentage)
            }

            return totalMaxScore > 0 ? (totalScore / totalMaxScore) * 100 : 0;
        }


        public double ComputeSubjectAverage()
        {
            double totalWeightedScore = 0;
            double totalMaxScore = 0;

            foreach (var quarter in Quarters)
            {
                double weightedQuarterScore = quarter.ComputeWeightedQuarterScore(Subject);
                totalWeightedScore += weightedQuarterScore; // Total weighted score for the subject
                totalMaxScore += 100; // Each quarter is out of 100 (percentage)
            }

            return totalMaxScore > 0 ? (totalWeightedScore / totalMaxScore) * 100 : 0;
        }

    }

    public class StudentRecord
    {
        public string Name { get; set; }
        public int GradeLevel { get; set; }
        public List<SubjectScore> SubjectScores { get; set; } = new List<SubjectScore>();

        public double ComputeOverallAverage()
        {
            double totalWeightedAverage = 0;
            int subjectCount = 0;

            foreach (var subject in SubjectScores)
            {
                totalWeightedAverage += subject.ComputeSubjectAverage();
                subjectCount++;
            }

            return subjectCount > 0 ? totalWeightedAverage / subjectCount : 0;
        }

        public double ComputeOverallQuarterAverage(int quarter)
        {
            double totalWeightedAverage = 0;
            int subjectCount = 0;

            foreach (var subject in SubjectScores)
            {
                var quarterScore = subject.Quarters.Find(q => q.Quarter == quarter);
                if (quarterScore != null)
                {
                    // Calculate the weighted score for this subject for the specified quarter
                    double weightedQuarterScore = quarterScore.ComputeWeightedQuarterScore(subject.Subject);
                    totalWeightedAverage += weightedQuarterScore; // Accumulate the weighted score
                    subjectCount++;
                }
            }

            // Return the average of the weighted scores
            return subjectCount > 0 ? totalWeightedAverage / subjectCount : 0;
        }

        public override string ToString()
        {
            return $"Student: {Name}, Grade Level: {GradeLevel}";
        }
    }

    class Program
    {
        static Dictionary<int, List<StudentRecord>> internalRecords = new()
        {
            { 7, new List<StudentRecord>() },
            { 8, new List<StudentRecord>() },
            { 9, new List<StudentRecord>() },
            { 10, new List<StudentRecord>() }
        };

        static string GetFilePath(int grade, int quarter)
        {
            string directoryPath = @"C:\Users\jadep\OneDrive\Desktop\ClassRecords"; // Updated path
            Directory.CreateDirectory(directoryPath); // Ensure the directory exists
            return Path.Combine(directoryPath, $"Grade{grade}_Quarter{quarter}_ClassRecord.txt");
        }

        static List<StudentRecord> LoadExistingRecords(int grade)
        {
            var existingRecords = new List<StudentRecord>();
            string studentFolderPath = @"C:\Users\jadep\OneDrive\Desktop\ClassRecords"; // Ensure this path is correct

            // Iterate through each student directory
            foreach (var studentDir in Directory.GetDirectories(studentFolderPath))
            {
                string studentName = Path.GetFileName(studentDir);
                var studentRecord = new StudentRecord { Name = studentName, GradeLevel = grade };

                // Load all quarters for the specified grade
                for (int quarter = 1; quarter <= 4; quarter++)
                {
                    string quarterFolderPath = Path.Combine(studentDir, $"Grade {grade} - Quarter {quarter}");

                    // Check if the quarter folder exists
                    if (Directory.Exists(quarterFolderPath))
                    {
                        foreach (var subjectFile in Directory.GetFiles(quarterFolderPath))
                        {
                            string subjectName = Path.GetFileNameWithoutExtension(subjectFile);
                            var subjectScore = new SubjectScore { Subject = subjectName };
                            studentRecord.SubjectScores.Add(subjectScore);

                            // Read the subject file
                            using (StreamReader sr = new StreamReader(subjectFile))
                            {
                                string line;
                                QuarterScore currentQuarter = new QuarterScore { Quarter = quarter };
                                subjectScore.Quarters.Add(currentQuarter);

                                while ((line = sr.ReadLine()) != null)
                                {
                                    line = line.Trim();
                                    if (line.Contains(":"))
                                    {
                                        var assessmentParts = line.Split(':');
                                        if (assessmentParts.Length == 2)
                                        {
                                            string assessmentType = assessmentParts[0].Trim();
                                            var scoreParts = assessmentParts[1].Split('/');
                                            if (scoreParts.Length == 2 &&
                                                int.TryParse(scoreParts[0].Trim(), out int score) &&
                                                int.TryParse(scoreParts[1].Trim(), out int maxScore))
                                            {
                                                currentQuarter.Assessments.Add(new Assessment
                                                {
                                                    Type = assessmentType,
                                                    Score = score,
                                                    MaxScore = maxScore
                                                });
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                existingRecords.Add(studentRecord);
            }

            return existingRecords;
        }

        static void Main()
        {
            var menu = new ScoreMenu();
            menu.DisplayWelcome();

            LoadRecords(); // Load existing records at the start

            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                menu.DisplayMainMenu();
                Console.Write("\nSelect an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddStudent();
                        break;
                    case "2":
                        UpdateStudent();
                        break;
                    case "3":
                        DeleteStudent();
                        break;
                    case "4":
                        SearchStudentRecord(); // This will trigger the new search functionality
                        break;
                    case "5":
                        Console.WriteLine("Exiting program...");
                        exit = true; // Exit the loop
                        break;
                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }

                if (!exit)
                {
                    Console.WriteLine("\nPress Enter to return to the menu...");
                    Console.ReadLine();
                }
            }
        }

        static void AddStudent()
        {
            Console.Clear();
            Console.WriteLine("=== Add Student ===");
            Console.Write("Enter Student Name: ");
            string name = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Error: Student name cannot be empty.");
                return;
            }

            Console.Write("Enter Grade Level (7-10): ");
            if (int.TryParse(Console.ReadLine(), out int grade) && grade >= 7 && grade <= 10)
            {
                var student = internalRecords[grade].Find(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
                if (student == null)
                {
                    student = new StudentRecord { Name = name, GradeLevel = grade };
                    internalRecords[grade].Add(student);
                    Console.WriteLine($"Student '{name}' added successfully.");
                }
                else
                {
                    Console.WriteLine($"Student '{name}' already exists in Grade {grade}.");
                }

                // Subject selection in closed-line tables
                string[] subjects = { "English", "Math", "Science", "Filipino", "AP", "MAPEH", "TLE", "ESP" };
                DisplaySubjectSelection(subjects);

                Console.Write("Choose a subject (1-8): ");
                if (int.TryParse(Console.ReadLine(), out int subjectChoice) && subjectChoice >= 1 && subjectChoice <= subjects.Length)
                {
                    string selectedSubject = subjects[subjectChoice - 1];
                    var subjectScore = student.SubjectScores.Find(s => s.Subject.Equals(selectedSubject, StringComparison.OrdinalIgnoreCase));
                    if (subjectScore == null)
                    {
                        subjectScore = new SubjectScore { Subject = selectedSubject };
                        student.SubjectScores.Add(subjectScore);
                        Console.WriteLine($"Subject '{selectedSubject}' added to {student.Name}'s records.");
                    }

                    // Quarter selection
                    Console.Clear();
                    Console.WriteLine("\nSelect the Quarter to Record (1-4): ");
                    if (int.TryParse(Console.ReadLine(), out int selectedQuarter) && selectedQuarter >= 1 && selectedQuarter <= 4)
                    {
                        var quarterScore = subjectScore.Quarters.Find(q => q.Quarter == selectedQuarter);
                        if (quarterScore != null)
                        {
                            Console.WriteLine($"An existing record for Quarter {selectedQuarter} of {selectedSubject} already exists. It will be overwritten.");
                            quarterScore.Assessments.Clear(); // Clear existing data for overwrite
                        }
                        else
                        {
                            quarterScore = new QuarterScore { Quarter = selectedQuarter };
                            subjectScore.Quarters.Add(quarterScore);
                        }

                        // Record assessments (with validation)
                        RecordAssessments(quarterScore);
                        Console.WriteLine("\nScores recorded successfully!");
                        SaveRecords(); // Save records after adding scores
                    }
                    else
                    {
                        Console.WriteLine("Error: Invalid quarter selection. Please enter a number between 1 and 4.");
                    }
                }
                else
                {
                    Console.WriteLine("Error: Invalid subject selection. Please enter a valid number.");
                }
            }
            else
            {
                Console.WriteLine("Error: Invalid grade level. Please enter a number between 7 and 10.");
            }
        }

        static void DisplaySubjectSelection(string[] subjects)
        {
            int windowWidth = Console.WindowWidth;
            int menuWidth = 30; // Define the width of the subject selection box
            int startPosition = (windowWidth / 2) - (menuWidth / 2);

            // Draw the subject selection title
            Console.SetCursorPosition(startPosition, Console.CursorTop);
            Console.WriteLine("   Select Subject to Record");

            // Display each subject in a closed-line table
            for (int i = 0; i < subjects.Length; i++)
            {
                // Draw the top border of the option box
                Console.SetCursorPosition(startPosition, Console.CursorTop);
                Console.WriteLine("┌" + new string('─', menuWidth - 2) + "┐");

                // Prepare the subject option with numbering
                string option = $"{i + 1}. {subjects[i]}";
                int optionLength = option.Length;

                // Center the option within the defined menu width
                int padding = (menuWidth - 2 - optionLength) / 2; // Calculate padding for centering
                string centeredOption = "│" + new string(' ', padding) + option + new string(' ', menuWidth - 2 - optionLength - padding) + "│";

                Console.SetCursorPosition(startPosition, Console.CursorTop);
                Console.WriteLine(centeredOption);

                // Draw the bottom border of the option box
                Console.SetCursorPosition(startPosition, Console.CursorTop);
                Console.WriteLine("└" + new string('─', menuWidth - 2) + "┘");
            }
        }

        // Helper method to record assessments
        static void RecordAssessments(QuarterScore quarterScore)
        {
            // Record Written Works
            Console.WriteLine("\nRecord Written Works:");
            int[] writtenMaxScores = { 20, 25, 20, 20, 25, 30, 20 };
            foreach (var maxScore in writtenMaxScores)
            {
                Console.Write($"Written Work ( /{maxScore}): ");
                if (int.TryParse(Console.ReadLine(), out int score) && score >= 0 && score <= maxScore)
                {
                    quarterScore.Assessments.Add(new Assessment
                    {
                        Type = "Written Work",
                        Score = score,
                        MaxScore = maxScore
                    });
                }
                else
                {
                    Console.WriteLine($"Invalid score. Please enter a score between 0 and {maxScore}.");
                }
            }

            // Record Performance Tasks
            Console.WriteLine("\nRecord Performance Tasks:");
            int[] performanceMaxScores = { 15, 15, 25, 20, 20, 25 };
            foreach (var maxScore in performanceMaxScores)
            {
                Console.Write($"Performance Task ( /{maxScore}): ");
                if (int.TryParse(Console.ReadLine(), out int score) && score >= 0 && score <= maxScore)
                {
                    quarterScore.Assessments.Add(new Assessment
                    {
                        Type = "Performance Task",
                        Score = score,
                        MaxScore = maxScore
                    });
                }
                else
                {
                    Console.WriteLine($"Invalid score. Please enter a score between 0 and {maxScore}.");
                }
            }

            // Record Quarterly Assessment
            Console.Write("\nQuarterly Assessment ( /50): ");
            if (int.TryParse(Console.ReadLine(), out int quarterlyAssessment) && quarterlyAssessment >= 0 && quarterlyAssessment <= 50)
            {
                // Check if a quarterly assessment already exists
                var existingQuarterlyAssessment = quarterScore.Assessments.FirstOrDefault(a => a.Type == "Quarterly Assessment");
                if (existingQuarterlyAssessment != null)
                {
                    // Update the existing assessment
                    existingQuarterlyAssessment.Score = quarterlyAssessment;
                }
                else
                {
                    // Add a new quarterly assessment
                    quarterScore.Assessments.Add(new Assessment
                    {
                        Type = "Quarterly Assessment",
                        Score = quarterlyAssessment,
                        MaxScore = 50
                    });
                }
            }
            else
            {
                Console.WriteLine("Invalid score. Please enter a score between 0 and 50.");
            }
        }

        static void UpdateStudent()
        {
            Console.Clear();
            Console.WriteLine("=== Update Student ===");
            Console.Write("Enter Grade Level of Student (7-10): ");
            if (int.TryParse(Console.ReadLine(), out int grade) && internalRecords.ContainsKey(grade))
            {
                // Load existing records for the specified grade
                var existingRecords = LoadExistingRecords(grade); // Load all quarters
                if (existingRecords.Count == 0)
                {
                    Console.WriteLine("No records found for this grade.");
                    return;
                }

                // Display existing records for the specified grade
                Console.WriteLine("Select a student to update:");
                for (int i = 0; i < existingRecords.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {existingRecords[i].Name}");
                }

                Console.Write("Enter the number of the student to update: ");
                if (int.TryParse(Console.ReadLine(), out int studentIndex) && studentIndex > 0 && studentIndex <= existingRecords.Count)
        {
                    var studentToUpdate = existingRecords[studentIndex - 1]; // Get the selected student
                    Console.WriteLine($"\nUpdating records for {studentToUpdate.Name}...");

                    // Allow the user to choose a quarter to update
                    Console.Write("\nEnter Quarter to Update (1-4): ");
                    if (int.TryParse(Console.ReadLine(), out int quarter) && quarter >= 1 && quarter <= 4)
                    {
                        // Display subjects for the selected student for the chosen quarter
                        var subjectsInQuarter = studentToUpdate.SubjectScores
                            .Where(subject => subject.Quarters.Any(q => q.Quarter == quarter))
                            .ToList();

                        if (subjectsInQuarter.Count == 0)
                        {
                            Console.WriteLine($"No subjects found for Quarter {quarter}.");
                            return;
                        }

                        Console.WriteLine("Select a subject to update:");
                        for (int i = 0; i < subjectsInQuarter.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {subjectsInQuarter[i].Subject}");
                        }

                        Console.Write("Enter the number of the subject to update: ");
                        if (int.TryParse(Console.ReadLine(), out int subjectIndex) && subjectIndex > 0 && subjectIndex <= subjectsInQuarter.Count)
                        {
                            var subjectToUpdate = subjectsInQuarter[subjectIndex - 1]; // Get the selected subject
                            var quarterScore = subjectToUpdate.Quarters.Find(q => q.Quarter == quarter);

                            if (quarterScore != null)
                            {
                                // Calculate initial weighted average
                                double initialWeightedAverage = quarterScore.ComputeWeightedQuarterScore(subjectToUpdate.Subject);
                                Console.WriteLine($"\nCurrent scores for {subjectToUpdate.Subject} in Quarter {quarter}:");
                                Console.WriteLine($"Initial Average: {initialWeightedAverage:F2}%");
                                foreach (var assessment in quarterScore.Assessments)
                                {
                                    Console.WriteLine($"  {assessment.Type}: {assessment.Score}/{assessment.MaxScore}");
                                }

                                // Prompt user for updates
                                Console.WriteLine("\nEnter new scores for assessments (leave blank to keep current):");
                                foreach (var assessment in quarterScore.Assessments)
                                {
                                    Console.Write($"Update {assessment.Type} (current: {assessment.Score}/{assessment.MaxScore}): ");
                                    string input = Console.ReadLine();
                                    if (int.TryParse(input, out int newScore) && newScore >= 0 && newScore <= assessment.MaxScore)
                                    {
                                        assessment.Score = newScore; // Update the score
                                    }
                                }

                                // Save updated records in the same directory structure
                                SaveUpdatedRecords(studentToUpdate, grade, quarter); // Save the updated records back to the file
                                Console.WriteLine("Records updated successfully!");

                                // Recalculate and save the overall quarterly average after the update
                                SaveQuarterlyAverage(studentToUpdate, grade, quarter);
                                LoadRecords(); // Reload records to reflect changes
                            }
                            else
                            {
                                Console.WriteLine($"No records found for {subjectToUpdate.Subject} in Quarter {quarter}.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid subject selection.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid quarter selection.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid student selection.");
                }
            }
            else
            {
                Console.WriteLine("Invalid grade level.");
            }
        }

        private static double CalculateAverage(List<Assessment> assessments)
        {
            if (assessments == null || assessments.Count == 0) return 0.0;

            double totalScore = 0;
            double totalMaxScore = 0;

            foreach (var assessment in assessments)
            {
                totalScore += assessment.Score;
                totalMaxScore += assessment.MaxScore;
            }

            return totalMaxScore > 0 ? totalScore / totalMaxScore * 100 : 0; // Return average } ```csharp
        }

        private static void SaveUpdatedRecords(StudentRecord student, int grade, int quarter)
        {
            string studentFolderPath = Path.Combine(@"C:\Users\jadep\OneDrive\Desktop\ClassRecords", student.Name);
            Directory.CreateDirectory(studentFolderPath); // Ensure the student folder exists

            foreach (var subjectScore in student.SubjectScores)
            {
                var quarterScore = subjectScore.Quarters.Find(q => q.Quarter == quarter);
                if (quarterScore != null)
                {
                    // Create a folder for the quarter if it doesn't exist
                    string quarterFolderPath = Path.Combine(studentFolderPath, $"Grade {grade} - Quarter {quarter}");
                    Directory.CreateDirectory(quarterFolderPath); // Ensure the quarter folder exists

                    // Save the updated scores to a file
                    string filePath = Path.Combine(quarterFolderPath, $"{subjectScore.Subject}.txt");
                    using (StreamWriter writer = new StreamWriter(filePath))
                    {
                        writer.WriteLine($"Subject: {subjectScore.Subject}");
                        writer.WriteLine($"Quarter: {quarter}");
                        foreach (var assessment in quarterScore.Assessments)
                        {
                            writer.WriteLine($"{assessment.Type}: {assessment.Score}/{assessment.MaxScore}");
                        }
                        // Calculate and write the new average
                        double weightedAverage = quarterScore.ComputeWeightedQuarterScore(subjectScore.Subject);
                        writer.WriteLine($"\nInitial Average: {weightedAverage:F2}%");
                    }
                }
            }
        }

        static void DeleteStudent()
        {
            Console.Clear();
            Console.WriteLine("=== Delete Student ===");
            Console.Write("Enter Grade Level of Student (7-10): ");

            if (int.TryParse(Console.ReadLine(), out int grade) && internalRecords.ContainsKey(grade))
            {
                var existingRecords = LoadExistingRecords(grade);
                if (existingRecords.Count == 0)
                {
                    Console.WriteLine("No records found for this grade.");
                    return;
                }

                // Step 1: Select Student
                Console.WriteLine("Select a student to delete:");
                for (int i = 0; i < existingRecords.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {existingRecords[i].Name}");
                }

                Console.Write("Enter the number of the student to delete: ");
                if (int.TryParse(Console.ReadLine(), out int studentIndex) && studentIndex > 0 && studentIndex <= existingRecords.Count)
                {
                    var studentToDelete = existingRecords[studentIndex - 1];
                    Console.WriteLine($"\nDeleting records for {studentToDelete.Name}...");

                    // Step 2: Select a quarter to delete from
                    Console.WriteLine("Select a quarter to delete from:");
                    for (int quarter = 1; quarter <= 4; quarter++)
                    {
                        Console.WriteLine($"{quarter}. Quarter {quarter}");
                    }

                    Console.Write("Enter the number of the quarter to delete from: ");
                    if (int.TryParse(Console.ReadLine(), out int quarterIndex) && quarterIndex >= 1 && quarterIndex <= 4)
                    {
                        // Step 3: Select a subject to delete
                        Console.WriteLine("Select a subject to delete:");
                        var quarterScore = studentToDelete.SubjectScores.SelectMany(s => s.Quarters)
                            .FirstOrDefault(q => q.Quarter == quarterIndex);

                        if (quarterScore != null)
                        {
                            for (int i = 0; i < studentToDelete.SubjectScores.Count; i++)
                            {
                                var subject = studentToDelete.SubjectScores[i];
                                if (subject.Quarters.Any(q => q.Quarter == quarterIndex))
                                {
                                    Console.WriteLine($"{i + 1}. {subject.Subject}");
                                }
                            }

                            Console.Write("Enter the number of the subject to delete: ");
                            if (int.TryParse(Console.ReadLine(), out int subjectIndex) && subjectIndex > 0 && subjectIndex <= studentToDelete.SubjectScores.Count)
                            {
                                var subjectToDelete = studentToDelete.SubjectScores[subjectIndex - 1];
                                if (subjectToDelete.Quarters.Any(q => q.Quarter == quarterIndex))
                                {
                                    Console.WriteLine($"Are you sure you want to delete the subject '{subjectToDelete.Subject}' for {studentToDelete.Name} in Quarter {quarterIndex}? (y/n): ");
                                    string confirmation = Console.ReadLine().Trim().ToLower();

                                    if (confirmation == "y")
                                    {
                                        // Step 4: Remove subject from internal records
                                        subjectToDelete.Quarters.RemoveAll(q => q.Quarter == quarterIndex);

                                        // Step 5: Delete the subject's records from the saved records directory
                                        string studentFolderPath = Path.Combine(@"C:\Users\jadep\OneDrive\Desktop\ClassRecords", studentToDelete.Name);
                                        string quarterFolderPath = Path.Combine(studentFolderPath, $"Grade {grade} - Quarter {quarterIndex}");
                                        string subjectFilePath = Path.Combine(quarterFolderPath, $"{subjectToDelete.Subject}.txt");

                                        if (File.Exists(subjectFilePath))
                                        {
                                            File.Delete(subjectFilePath);
                                            Console.WriteLine($"Deleted records for '{subjectToDelete.Subject}' in {studentToDelete.Name}'s records for Grade {grade}, Quarter {quarterIndex}.");
                                        }

                                        Console.WriteLine($"Successfully deleted subject '{subjectToDelete.Subject}' for {studentToDelete.Name} in Quarter {quarterIndex}.");

                                        // Update the overall quarterly average after deletion
                                        SaveQuarterlyAverage(studentToDelete, grade, quarterIndex); // Ensure to save the updated average
                                    }
                                    else
                                    {
                                        Console.WriteLine("Deletion canceled.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine($"No records found for subject '{subjectToDelete.Subject}' in Quarter {quarterIndex}.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid subject selection.");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"No subjects found for Quarter {quarterIndex}.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid quarter selection.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid student selection.");
                }
            }
            else
            {
                Console.WriteLine("Invalid grade level.");
            }
        }

        // Method to save updated internal records to the specified file path
        private static void SaveUpdatedInternalRecords(int grade, int quarter)
        {
            string filePath = GetFilePath(grade, quarter);
            const int maxRetries = 5;
            const int delayBetweenRetries = 1000; // milliseconds

            for (int attempt = 0; attempt < maxRetries; attempt++)
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(filePath, append: false))
                    {
                        foreach (var student in internalRecords[grade])
                        {
                            if (!string.IsNullOrWhiteSpace(student.Name))
                            {
                                sw.WriteLine($"Student: {student.Name}, Grade Level: {student.GradeLevel}");
                                foreach (var subjectScore in student.SubjectScores)
                                {
                                    var quarterScore = subjectScore.Quarters.Find(q => q.Quarter == quarter);
                                    if (quarterScore != null)
                                    {
                                        sw.WriteLine($"Subject: {subjectScore.Subject}");
                                        sw.WriteLine($"Quarter {quarter}:");

                                        // Check if there are assessments, including quarterly assessments
                                        if (quarterScore.Assessments.Any())
                                        {
                                            foreach (var assessment in quarterScore.Assessments)
                                            {
                                                sw.WriteLine($"{assessment.Type}: {assessment.Score}/{assessment.MaxScore}");
                                            }
                                        }
                                        else
                                        {
                                            sw.WriteLine("No assessments available for this quarter.");
                                        }
                                    }
                                }
                            }
                        }
                    }
                    Console.WriteLine($"Updated internal records for Grade {grade}, Quarter {quarter} saved to {filePath}");
                    break; // Exit the retry loop if successful
                }
                catch (IOException ex)
                {
                    Console.WriteLine($"Attempt {attempt + 1}: An I/O error occurred while writing to the file: {ex.Message}");
                    System.Threading.Thread.Sleep(delayBetweenRetries); // Wait before retrying
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                    break; // Exit on unexpected error
                }
            }
        }

        static void SearchStudentRecord()
        {
            Console.Clear();
            Console.Write("Enter the student's name: ");
            string studentName = Console.ReadLine().Trim();

            Console.Write("Enter the grade (7-10): ");
            if (int.TryParse(Console.ReadLine(), out int grade) && internalRecords.ContainsKey(grade))
            {
                Console.Write("Enter the quarter (1-4): ");
                if (int.TryParse(Console.ReadLine(), out int quarter) && quarter >= 1 && quarter <= 4)
                {
                    // Ask for the subject to search
                    Console.Write("Enter the subject: ");
                    string subjectName = Console.ReadLine().Trim();

                    // Load external records
                    var externalRecords = LoadExternalRecords(grade, quarter); // Load all records for the grade
                    var studentInExternal = externalRecords.Find(s => s.Name.Equals(studentName, StringComparison.OrdinalIgnoreCase));
                    if (studentInExternal != null)
                    {
                        var subjectInExternal = studentInExternal.SubjectScores.Find(s => s.Subject.Equals(subjectName, StringComparison.OrdinalIgnoreCase));
                        if (subjectInExternal != null)
                        {
                            var quarterScore = subjectInExternal.Quarters.Find(q => q.Quarter == quarter);
                            if (quarterScore != null)
                            {
                                Console.WriteLine($"\n=== External Records for {studentInExternal.Name} ===");
                                DisplayStudentRecordsForSubject(studentInExternal, subjectInExternal, quarter);
                            }
                            else
                            {
                                Console.WriteLine($"No records found for {subjectName} in Quarter {quarter} in external database.");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"No records found for subject {subjectName} in external database for {studentInExternal.Name}.");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"No records found in external database for {studentName}.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid quarter. Please enter a number between 1 and 4.");
                }
            }
            else
            {
                Console.WriteLine("Invalid grade. Please enter a number between 7 and 10.");
            }
        }

        static void DisplayStudentRecordsForSubject(StudentRecord student, SubjectScore subject, int quarter)
        {
            Console.WriteLine($"Student: {student.Name}, Grade Level: {student.GradeLevel}");
            Console.WriteLine($"Subject: {subject.Subject}, Quarter: {quarter}");

            // Display assessments for the specified quarter
            var quarterScore = subject.Quarters.Find(q => q.Quarter == quarter);
            if (quarterScore != null)
            {
                foreach (var assessment in quarterScore.Assessments)
                {
                    Console.WriteLine($"  {assessment.Type}: {assessment.Score}/{assessment.MaxScore}");
                }

                // Calculate and display the weighted average for the subject
                double weightedAverage = quarterScore.ComputeWeightedQuarterScore(subject.Subject);
                Console.WriteLine($"Initial Average for {subject.Subject} in Quarter {quarter}: {weightedAverage:F2}%");
            }
            else
            {
                Console.WriteLine($"No assessments found for Quarter {quarter}.");
            }
        }

        static List<StudentRecord> LoadExternalRecords(int grade, int quarter)
        {
            var externalRecords = new List<StudentRecord>();
            string studentFolderPath = @"C:\Users\jadep\OneDrive\Desktop\ClassRecords"; // Change this path if necessary

            // Iterate through each student directory
            foreach (var studentDir in Directory.GetDirectories(studentFolderPath))
            {
                string studentName = Path.GetFileName(studentDir);
                var studentRecord = new StudentRecord { Name = studentName, GradeLevel = grade };

                // Construct the path for the specific quarter folder
                string quarterFolderPath = Path.Combine(studentDir, $"Grade {grade} - Quarter {quarter}");

                // Check if the quarter folder exists
                if (Directory.Exists(quarterFolderPath))
                {
                    // Iterate through each subject file in the quarter folder
                    foreach (var subjectFile in Directory.GetFiles(quarterFolderPath))
                    {
                        string subjectName = Path.GetFileNameWithoutExtension(subjectFile);
                        var subjectScore = new SubjectScore { Subject = subjectName };
                        studentRecord.SubjectScores.Add(subjectScore);

                        // Read the subject file
                        using (StreamReader sr = new StreamReader(subjectFile))
                        {
                            string line;
                            QuarterScore currentQuarter = new QuarterScore { Quarter = quarter };
                            subjectScore.Quarters.Add(currentQuarter);

                            while ((line = sr.ReadLine()) != null)
                            {
                                line = line.Trim();
                                if (line.Contains(":"))
                                {
                                    var assessmentParts = line.Split(':');
                                    if (assessmentParts.Length == 2)
                                    {
                                        string assessmentType = assessmentParts[0].Trim();
                                        var scoreParts = assessmentParts[1].Split('/');
                                        if (scoreParts.Length == 2 &&
                                            int.TryParse(scoreParts[0].Trim(), out int score) &&
                                            int.TryParse(scoreParts[1].Trim(), out int maxScore))
                                        {
                                            currentQuarter.Assessments.Add(new Assessment
                                            {
                                                Type = assessmentType,
                                                Score = score,
                                                MaxScore = maxScore
                                            });
                                        }
                                    }
                                }
                            }
                        }
                    }
                    externalRecords.Add(studentRecord); // Add the student record to the list
                }
            }

            return externalRecords;
        }

        static void SaveRecords()
        {
            foreach (var grade in internalRecords.Keys)
            {
                foreach (var student in internalRecords[grade])
                {
                    // Create a folder for the student
                    string studentFolderPath = Path.Combine(@"C:\Users\jadep\OneDrive\Desktop\ClassRecords", student.Name);
                    Directory.CreateDirectory(studentFolderPath); // Create student folder

                    for (int quarter = 1; quarter <= 4; quarter++)
                    {
                        // Create a folder for each quarter specific to the grade level
                        string quarterFolderPath = Path.Combine(studentFolderPath, $"Grade {grade} - Quarter {quarter}");
                        Directory.CreateDirectory(quarterFolderPath); // Create quarter folder

                        // Save subject records for the current quarter
                        foreach (var subjectScore in student.SubjectScores)
                        {
                            var quarterScore = subjectScore.Quarters.Find(q => q.Quarter == quarter);
                            if (quarterScore != null)
                            {
                                // Create a text file for the subject
                                string subjectFilePath = Path.Combine(quarterFolderPath, $"{subjectScore.Subject}.txt");

                                using (StreamWriter sw = new StreamWriter(subjectFilePath, append: false))
                                {
                                    sw.WriteLine($"Student: {student.Name}  \nGrade Level: {student.GradeLevel}");
                                    sw.WriteLine($"\nSubject: {subjectScore.Subject}");
                                    sw.WriteLine($"\nQuarter {quarter}:");

                                    foreach (var assessment in quarterScore.Assessments)
                                    {
                                        sw.WriteLine($"{assessment.Type}: {assessment.Score}/{assessment.MaxScore}");
                                    }

                                    // Calculate and display the weighted average
                                    double weightedAverage = quarterScore.ComputeWeightedQuarterScore(subjectScore.Subject);
                                    sw.WriteLine($"\nInitial Average for Quarter {quarter}: {weightedAverage:F2}%");
                                }
                                Console.WriteLine($"Records for {student.Name}, {subjectScore.Subject} for Quarter {quarter} (Grade {grade}) saved to {subjectFilePath}");
                            }
                        }

                        // Calculate and save the overall quarterly average for this quarter
                        SaveQuarterlyAverage(student, grade, quarter);
                    }
                }
            }
        }

        private static void SaveQuarterlyAverage(StudentRecord student, int grade, int quarter)
        {
            string studentFolderPath = Path.Combine(@"C:\Users\jadep\OneDrive\Desktop\ClassRecords", student.Name);
            string overallAverageFilePath = Path.Combine(studentFolderPath, $"OverallQuarterlyAverage_Grade{grade}_Quarter{quarter}.txt");

            double overallQuarterAverage = student.ComputeOverallQuarterAverage(quarter);

            using (StreamWriter sw = new StreamWriter(overallAverageFilePath, append: false))
            {
                sw.WriteLine($"Overall Quarterly Average for {student.Name} in Grade {grade}, Quarter {quarter}: {overallQuarterAverage:F2}%");
            }

            Console.WriteLine($"Overall Quarterly Average for {student.Name} in Grade {grade}, Quarter {quarter} saved to {overallAverageFilePath}");
        }

        private static void SaveSubjectRecords(StreamWriter sw, StudentRecord student, int quarter)
        {
            foreach (var subjectScore in student.SubjectScores)
            {
                var quarterScore = subjectScore.Quarters.Find(q => q.Quarter == quarter);
                if (quarterScore != null)
                {
                    sw.WriteLine($"\nSubject: {subjectScore.Subject}");
                    sw.WriteLine($"Quarter {quarter}:");

                    // Calculate the initial average for this subject
                    double initialAverage = quarterScore.ComputeQuarterAverage();
                    sw.WriteLine($"Initial Average: {initialAverage:F2}%");

                    foreach (var assessment in quarterScore.Assessments)
                    {
                        sw.WriteLine($"{assessment.Type}: {assessment.Score}/{assessment.MaxScore}");
                    }
                }
            }

            double overallAverage = student.ComputeOverallAverage();
            sw.WriteLine($"\nOverall Average for {student.Name}: {overallAverage:F2}%");
        }

        static void ViewRecords()
        {
            Console.Write("Enter Grade Level to View Records (7-10): ");
            if (int.TryParse(Console.ReadLine(), out int grade) && internalRecords.ContainsKey(grade))
            {
                Console.Write("Enter Student Name: ");
                string studentName = Console.ReadLine()?.Trim();

                var student = internalRecords[grade].Find(s => s.Name.Equals(studentName, StringComparison.OrdinalIgnoreCase));
                if (student != null)
                {
                    Console.WriteLine($"=== Records for Student: {student.Name} (Grade {student.GradeLevel}) ===");

                    foreach (var subject in student.SubjectScores)
                    {
                        Console.WriteLine($"Subject: {subject.Subject}");
                        double totalWeightedAverage = 0;
                        int quarterCount = 0;

                        foreach (var quarter in subject.Quarters)
                        {
                            Console.WriteLine($"  Quarter {quarter.Quarter}:");
                            foreach (var assessment in quarter.Assessments)
                            {
                                Console.WriteLine($"    {assessment.Type}: {assessment.Score}/{assessment.MaxScore}");
                            }

                            double weightedQuarterAverage = quarter.ComputeWeightedQuarterScore(subject.Subject); // Use weighted average
                            Console.WriteLine($"    Weighted Average for Quarter {quarter.Quarter}: {weightedQuarterAverage:F2}%");
                            totalWeightedAverage += weightedQuarterAverage;
                            quarterCount++;
                        }

                        if (quarterCount > 0)
                        {
                            double finalWeightedAverage = totalWeightedAverage / quarterCount; // Final weighted average
                            Console.WriteLine($"  Final Weighted Average for {subject.Subject}: {finalWeightedAverage:F2}%");
                        }
                    }

                    double overallWeightedAverage = student.ComputeOverallAverage(); // Overall weighted average
                    Console.WriteLine($"Overall Weighted Average for {student.Name}: {overallWeightedAverage:F2}%");
                }
                else
                {
                    Console.WriteLine("Student not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid grade level.");
            }
        }

        static void LoadRecords()
        {
            foreach (var grade in internalRecords.Keys)
            {
                for (int quarter = 1; quarter <= 4; quarter++)
                {
                    string filePath = GetFilePath(grade, quarter);
                    if (File.Exists(filePath))
                    {
                        using (StreamReader sr = new StreamReader(filePath))
                        {
                            string line;
                            StudentRecord currentStudent = null;
                            SubjectScore currentSubject = null;
                            QuarterScore currentQuarter = null;

                            // Clear existing records for the current grade before loading
                            internalRecords[grade].Clear();

                            while ((line = sr.ReadLine()) != null)
                            {
                                line = line.Trim();

                                if (line.StartsWith("Student:"))
                                {
                                    var parts = line.Split(',');
                                    if (parts.Length >= 2)
                                    {
                                        string name = parts[0].Substring("Student: ".Length).Trim();
                                        if (int.TryParse(parts[1].Substring("Grade Level: ".Length).Trim(), out int gradeLevel))
                                        {
                                            currentStudent = new StudentRecord { Name = name, GradeLevel = gradeLevel };
                                            internalRecords[grade].Add(currentStudent);
                                        }
                                    }
                                }
                                else if (line.StartsWith("Subject:") && currentStudent != null)
                                {
                                    string subjectName = line.Substring("Subject: ".Length).Trim();
                                    currentSubject = new SubjectScore { Subject = subjectName };
                                    currentStudent.SubjectScores.Add(currentSubject);
                                }
                                else if (line.StartsWith("Quarter") && currentSubject != null)
                                {
                                    string[] parts = line.Split(" ");
                                    if (parts.Length > 1 && int.TryParse(parts[1].TrimEnd(':'), out int quarterIndex))
                                    {
                                        currentQuarter = new QuarterScore { Quarter = quarterIndex };
                                        currentSubject.Quarters.Add(currentQuarter);
                                    }
                                }
                                else if (currentQuarter != null && line.Contains(":"))
                                {
                                    var assessmentParts = line.Split(':');
                                    if (assessmentParts.Length == 2)
                                    {
                                        string assessmentType = assessmentParts[0].Trim();
                                        var scoreParts = assessmentParts[1].Split('/');
                                        if (scoreParts.Length == 2 &&
                                            int.TryParse(scoreParts[0].Trim(), out int score) &&
                                            int.TryParse(scoreParts[1].Trim(), out int maxScore))
                                        {
                                            currentQuarter.Assessments.Add(new Assessment
                                            {
                                                Type = assessmentType,
                                                Score = score,
                                                MaxScore = maxScore
                                            });
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}