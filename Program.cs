using MandatApp;

Console.ForegroundColor = ConsoleColor.Green;

await RetrieveWebPage.GetRetrievedPage(337);
/*for(int i = 300; i <= 9160; i++)
{
    if (await RetrieveWebPage.GetRetrievedPage(i))
        break;
}*/