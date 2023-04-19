#load "common_lib.csx"
using System;
using System.IO;
using System.Diagnostics;

public void CopyFolder(string sourceFolder, string destFolder)
{
    if (Directory.Exists(destFolder) is false)
        Directory.CreateDirectory(destFolder);

    var files = Directory.EnumerateFiles(sourceFolder);
    foreach (string file in files)
        File.Copy(file, Path.Combine(destFolder, Path.GetFileName(file)), true);

    var folders = Directory.EnumerateDirectories(sourceFolder);
    foreach (string folder in folders)
        CopyFolder(folder, Path.Combine(destFolder, Path.GetFileName(folder)));
}
try
{
    const string CelesteModFolder = @"C:\Program Files (x86)\Steam\steamapps\common\Celeste\Mods\SalKeyOverlayer";
    File.Copy(@"SalKeyOverlayer\bin\x86\Debug\SalKeyOverlayer.dll", @"ModPack\SalKeyOverlayer.dll", true);
    File.Copy(@"SalKeyOverlayer\bin\x86\Debug\SalKeyOverlayer.pdb", @"ModPack\SalKeyOverlayer.pdb", true);
    CopyFolder(@"ModPack", CelesteModFolder);
}
catch (Exception e)
{
    Console.Error.WriteLine(e);
}