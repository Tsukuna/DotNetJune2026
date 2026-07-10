// See https://aka.ms/new-console-template for more information
using TestingConsoleApp;

Console.WriteLine("Dapper Starting...");

DapperService dapperService = new DapperService();

//dapperService.Create();
dapperService.GetData();
//dapperService.Update();
//dapperService.Delete();