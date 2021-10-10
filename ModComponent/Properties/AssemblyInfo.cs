using MelonLoader;
using System.Reflection;
using System.Runtime.InteropServices;
using BuildInfo = ModComponentMain.BuildInfo;

[assembly: ComVisible(false)]
[assembly: Guid("b809c688-2a3f-4573-821f-fb90ea433dad")]

[assembly: AssemblyTitle(BuildInfo.Name)]
[assembly: AssemblyDescription(BuildInfo.Description)]
[assembly: AssemblyCompany(BuildInfo.Company)]
[assembly: AssemblyProduct(BuildInfo.Name)]
[assembly: AssemblyCopyright("Created by " + BuildInfo.Author)]
[assembly: AssemblyTrademark(BuildInfo.Company)]
[assembly: AssemblyCulture("")]

[assembly: AssemblyVersion(BuildInfo.Version)]
[assembly: AssemblyFileVersion(BuildInfo.Version)]
[assembly: MelonInfo(typeof(ModComponentMain.Implementation), BuildInfo.Name, BuildInfo.Version, BuildInfo.Author, BuildInfo.DownloadLink)]
[assembly: MelonGame("Hinterland", "TheLongDark")]
