using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;

class Member
{
    public string Name { get; set; }
    public string Department { get; set; }
    public string ID { get; set; }
    public int Level { get; set; }
    public string LevelName { get; set; }
    public string Title { get; set; } = "無"; // 職稱預設為 "無"
}

class Program
{
    static List<Member> members = new List<Member>();

    static void Main(string[] args)
    {
        bool loopDone = false;
        Console.WriteLine("-------------------###社員登記系統###-------------------");
        Console.WriteLine("新增社員資訊:   register       name    department  ID");
        Console.WriteLine("以特定屬性查詢: search         name    tag     Want_search_string");
        Console.WriteLine("授予社員職位:   entitle        name    department  ID  That_title");
        Console.WriteLine("所有社員列表:   check");
        Console.WriteLine("指令格式列表:   help");
        Console.WriteLine("離開此程式:     exit ");
        while (!loopDone)
        {

            string option = Console.ReadLine();
            string[] inputParts = option.Split(' ');


            switch (inputParts[0])
            {
                case "register":
                    RegisterMember(option);
                    break;

                case "search":
                    SearchMenu(option);
                    break;

                case "entitle":
                    SetTitle(option);
                    break;

                case "check":
                    ShowMembers();
                    break;

                case "help":
                    ShowHelp();
                    break;

                case "exit":
                    loopDone = true;
                    break;

                default:
                    Console.WriteLine("無效的指令，請重新輸入。");
                    break;
            }
        }
    }

    static void RegisterMember(string option)
    {
        //Console.Write("請輸入命令和社員資訊 (register 名字 科系 學號): ");
        string[] inputParts = option.Split(' ');
        if (inputParts.Length != 4 || inputParts[0] != "register")
        {
            Console.WriteLine("請輸入正確的格式 (register 名字 科系 學號)。");
            return;
        }
        // 將輸入按空格分割成部分


        string name = inputParts[1];
        string department = inputParts[2];
        string id = inputParts[3];

        // 檢查是否已存在相同的社員
        bool memberExists = members.Any(member => member.Name == name && member.Department == department && member.ID == id);

        if (memberExists)
        {
            // 更新等級
            Member existingMember = members.First(member => member.Name == name && member.Department == department && member.ID == id);
            existingMember.Level++;

            // 設定職稱
            if (existingMember.Level == 1)
            {
                existingMember.LevelName = "盟新社員";
            }
            else if (existingMember.Level == 2)
            {
                existingMember.LevelName = "資深社員";
            }
            else if (existingMember.Level >= 3)
            {
                existingMember.LevelName = "永久社員";
            }

            Console.WriteLine("社員已登記，並更新等級及職稱。");
        }
        else
        {
            Member newMember = new Member
            {
                Name = name,
                Department = department,
                ID = id,
                Level = 1,
                LevelName = "盟新社員"
            };

            members.Add(newMember);
            Console.WriteLine("新社員已登記。");
        }
    }


    static void SearchByName(string nameToSearch)
    {
        var matchingMembers = members.Where(member => member.Name == nameToSearch).ToList();

        if (matchingMembers.Count == 0)
        {
            Console.WriteLine($"找不到名字為 {nameToSearch} 的社員。");
        }
        else
        {
            Console.WriteLine($"搜尋結果 ({nameToSearch} 的社員資訊)：");
            foreach (var member in matchingMembers)
            {
                Console.WriteLine($"名字: {member.Name}, 科系: {member.Department}, 學號: {member.ID}, 等級: {member.LevelName}, 職稱: {member.Title}");
            }
        }
    }

    static void SearchByDepartment(string departmentToSearch)
    {
        var matchingMembers = members.Where(member => member.Department == departmentToSearch).ToList();

        if (matchingMembers.Count == 0)
        {
            Console.WriteLine($"找不到科系為 {departmentToSearch} 的社員。");
        }
        else
        {
            Console.WriteLine($"搜尋結果 (科系為 {departmentToSearch} 的社員資訊)：");
            foreach (var member in matchingMembers)
            {
                Console.WriteLine($"名字: {member.Name}, 科系: {member.Department}, 學號: {member.ID}, 等級: {member.LevelName}, 職稱: {member.Title}");
            }
        }
    }

    static void SearchByID(string idToSearch)
    {
        var matchingMember = members.FirstOrDefault(member => member.ID == idToSearch);

        if (matchingMember != null)
        {
            Console.WriteLine($"搜尋結果 (學號為 {idToSearch} 的社員資訊)：");
            Console.WriteLine($"名字: {matchingMember.Name}, 科系: {matchingMember.Department}, 學號: {matchingMember.ID}, 等級: {matchingMember.LevelName}, 職稱: {matchingMember.Title}");
        }
        else
        {
            Console.WriteLine($"找不到學號為 {idToSearch} 的社員。");
        }
    }

    static void SearchByLevelName(string levelnameToSearch)
    {

        var matchingMembers = members.Where(member => member.LevelName == levelnameToSearch).ToList();

        if (matchingMembers.Count == 0)
        {
            Console.WriteLine($"找不到等級為 {levelnameToSearch} 的社員。");
        }
        else
        {
            Console.WriteLine($"搜尋結果 (等級為 {levelnameToSearch} 的社員資訊)：");
            foreach (var member in matchingMembers)
            {
                Console.WriteLine($"名字: {member.Name}, 科系: {member.Department}, 學號: {member.ID}, 等級: {member.LevelName}, 職稱: {member.Title}");
            }
        }
    }


    static void SearchMenu(string option)
    {

        string[] inputParts = option.Split(' ');

        if (inputParts.Length < 3 || inputParts[0] != "search")
        {
            Console.WriteLine("請輸入正確的格式 (search name 簡震業)。");
            return;
        }

        string searchOption = inputParts[1];
        string searchValue = inputParts[2];

        switch (searchOption)
        {
            case "name":
                SearchByName(searchValue);
                break;
            // 添加其他搜尋選項的處理
            case "department":
                SearchByDepartment(searchValue);
                break;
            case "ID":
                SearchByID(searchValue); 
                break;
            case "level":
                //int searchvalue = int.Parse(searchValue);
                SearchByLevelName(searchValue);
                break;
            default:
                Console.WriteLine("無效的搜尋選項，請重新輸入。");
                break;
        }
    }


    static void SetTitle(string option)
    {
        string[] inputParts = option.Split(' ');
        //Console.Write("請輸入社員名字: ");
        string name = inputParts[1];
        //Console.Write("請輸入新職稱: ");
        string newTitle = inputParts[2];

        Member member = members.FirstOrDefault(m => m.Name == name);
        if(newTitle == "社長大大")
        {
            Console.WriteLine("不可能!!!社長大大只有一個人");
        }
        else
        {
            if (member != null)
            {
                member.Title = newTitle;
                Console.WriteLine($"{member.Name} 的職稱已設定為 {newTitle}。");
            }
            else
            {
                Console.WriteLine("找不到對應的社員，請檢查名字是否正確。");
            }
        }

    }

    static void ShowMembers()
    {
        Console.WriteLine("所有社員列表：");
        foreach (Member member in members)
        {
            Console.WriteLine($"名字: {member.Name}, 科系: {member.Department}, 學號: {member.ID}, 等級: {member.LevelName}, 職稱: {member.Title}");
        }
    }

    static void ShowHelp()
    {
        Console.WriteLine("-------------------###社員登記系統###-------------------");
        Console.WriteLine("新增社員資訊:   register       name    department  ID");
        Console.WriteLine("以特定屬性查詢: search         name    tag     Want_search_string");
        Console.WriteLine("授予社員職位:   entitle        name    department  ID  That_title");
        Console.WriteLine("所有社員列表:   check");
        Console.WriteLine("指令格式列表:   help");
        Console.WriteLine("離開此程式:     exit ");
    }

    // 實現其他功能的方法

}
