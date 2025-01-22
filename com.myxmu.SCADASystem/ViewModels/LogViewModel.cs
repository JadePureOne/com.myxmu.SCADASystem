﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.myxmu.SCADASystem.ViewModels
{
    public partial class LogViewModel:ObservableObject
    {
        [ObservableProperty]
        ObservableCollection<FileInfo> _logFiles = new();

        [ObservableProperty]
        FileInfo _selectedLogFile;

        partial void OnSelectedLogFileChanged(FileInfo value)
        {
            //if (value != null)
            //{
            //    //ObservableCollection<LogEntry> entries = new ();
            //    var lines = File.ReadAllLines(path: value.FullName);
            //    foreach (var line in lines)
            //    {
            //        var entry = LogEntry.Parse(line);
            //        if (entry != null)
            //        {
            //            LogEntries.Add(entry);
            //        }
            //    }
            //}
            LogEntries.Clear();
            if (value != null)
            {
                try
                {
                    // Open the file for reading with shared access
                    using (var fileStream = new FileStream(value.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    using (var reader = new StreamReader(fileStream))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            var entry = LogEntry.Parse(line);
                            if (entry != null)
                            {
                                LogEntries.Add(entry);
                            }
                        }
                    }
                }
                catch (IOException ex)
                {
                    // Handle the exception (e.g., log it or show a message to the user)
                    Console.WriteLine($"An error occurred while reading the file: {ex.Message}");
                }
            }
        }

        [ObservableProperty]
        ObservableCollection<LogEntry> _logEntries = new();    

        [RelayCommand]
        void OpenLogFolder() 
        {
            try
            {
                LogFiles.Clear();

                var logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");

                if (Directory.Exists(logPath))
                {
                    // 遍历一周的日志文件
                    var currentDate = DateTime.Now;
                    var oneWeekAgo = currentDate.AddDays(-30);

                    // 获取所有以日期命名的子文件夹
                    var dateFolders = Directory.GetDirectories(logPath)
                        .Where(dir => DateTime.TryParse(Path.GetFileName(dir), out _))
                        .Select(dir => new DirectoryInfo(dir));

                    var recentFolders = dateFolders.Where(dir =>
                    {
                        if (DateTime.TryParse(dir.Name, out DateTime folderDate))
                        {
                            return folderDate >= oneWeekAgo && folderDate <= currentDate;
                        }

                        return false;
                    });

                    // 获取满足日期的文件夹下的所有日志文件
                    var logFiles = recentFolders.SelectMany(dir =>
                    dir.GetFiles("*.log", SearchOption.AllDirectories))
                        .OrderByDescending(f => f.LastWriteTime);

                    LogFiles = new ObservableCollection<FileInfo>(logFiles);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }

        }
    }
}
