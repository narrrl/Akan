using Discord;
using Discord.Commands;
using System;
using System.Threading.Tasks;
using JikanDotNet;
using Akan.Services;

namespace Akan.Module
{
    [Group("mal")]
    public class Anime : ModuleBase<SocketCommandContext>
    {
        static int yearCurrently = Methods.getYear();
        static string seasonCurrently = Methods.getSeason();
        [Command("season")]
        public async Task Season(int year = 0, string seasonStr = null)
        {
            if(year == 0)
            {
                year = yearCurrently;
            }
            if(seasonStr == null)
            {
                seasonStr = seasonCurrently;
                if (seasonStr.Equals("winter"))
                {
                    year++;
                }
            }
            IJikan jikan = new Jikan(true);

            Season season;
            switch (seasonStr)
            {
                case "spring":
                    season = jikan.GetSeason(year,Seasons.Spring).Result;
                    break;
                case "summer":
                    season = jikan.GetSeason(year, Seasons.Summer).Result;
                    break;
                case "fall":
                    season = jikan.GetSeason(year, Seasons.Fall).Result;
                    break;
                case "winter":
                    season = jikan.GetSeason(year, Seasons.Winter).Result;
                    break;
                default:
                    await ReplyAsync("Usage: **akan!mal** **season** + [**year**] + [**spring**/**summer**/**fall**/**winter**]");
                    return;
            }



            EmbedBuilder seasonEmb = new EmbedBuilder();

            string animeList = "";
            foreach (var seasonEntry in season.SeasonEntries)
            {
                animeList = animeList + "[" + seasonEntry.Title + "](" + seasonEntry.URL + ")" + "\n";
            }

            string[] slicedList = animeList.Split("\n");
            string finalAnimeList = "";

            for(int i = 0; i <= slicedList.Length - 1 && finalAnimeList.Length < 1900; i++)
            {
                finalAnimeList = finalAnimeList + slicedList[i] + "\n";
            }

            seasonEmb.WithTitle("Season : " + season.SeasonYear + " " + season.SeasonName)
                     .WithDescription(finalAnimeList)
                     .WithColor(0x2e51a2);

            await ReplyAsync("", false, seasonEmb.Build());

            
        }

        [Command("nextSeason")]
        public async Task SeasonNext()
        {
            int year = Methods.getYear();
            string seasonStr = Methods.getNextSeason();
            if (seasonStr.Equals("winter"))
            {
                year++;
            }
            IJikan jikan = new Jikan(true);

            Season season;
            switch (seasonStr)
            {
                case "spring":
                    season = jikan.GetSeason(year, Seasons.Spring).Result;
                    break;
                case "summer":
                    season = jikan.GetSeason(year, Seasons.Summer).Result;
                    break;
                case "fall":
                    season = jikan.GetSeason(year, Seasons.Fall).Result;
                    break;
                case "winter":
                    season = jikan.GetSeason(year, Seasons.Winter).Result;
                    break;
                default:
                    await ReplyAsync("Usage: **akan!mal** **season** + [**year**] + [**spring**/**summer**/**fall**/**winter**]");
                    return;
            }



            EmbedBuilder seasonEmb = new EmbedBuilder();

            string animeList = "";
            foreach (var seasonEntry in season.SeasonEntries)
            {
                animeList = animeList + "[" + seasonEntry.Title + "](" + seasonEntry.URL + ")" + "\n";
            }

            string[] slicedList = animeList.Split("\n");
            string finalAnimeList = "";

            for (int i = 0; i <= slicedList.Length - 1 && finalAnimeList.Length < 1900; i++)
            {
                finalAnimeList = finalAnimeList + slicedList[i] + "\n";
            }

            seasonEmb.WithTitle("Season : " + season.SeasonYear + " " + season.SeasonName)
                     .WithDescription(finalAnimeList)
                     .WithColor(0x2e51a2);

            await ReplyAsync("", false, seasonEmb.Build());


        }

        [Command("top")]
        public async Task TopAnime([Remainder]string str = "1")
        {
            str = str.ToLower();
            IJikan jikan = new Jikan();
            AnimeTop topAnimeList;
            if (str.Length <= 5)
            {
                int pageNum = Convert.ToInt32(str);
                topAnimeList = await jikan.GetAnimeTop(pageNum);
            }
            else if(str.Equals("airing"))
            {

                topAnimeList = await jikan.GetAnimeTop(TopAnimeExtension.TopAiring);
            }
            else if (str.Equals("upcoming"))
            {
                topAnimeList = await jikan.GetAnimeTop(TopAnimeExtension.TopUpcoming);
            }
            else
            {
                await ReplyAsync("Error!");
                return;
            }

            int place = 1;
            //string animeList = "";
            string[] list = new string[50];
            int index = 0;
            foreach (var topAnime in topAnimeList.Top)
            {
                list[index] = place.ToString() + " [" + topAnime.Title + "](" + topAnime.Url + ")" + "\n";
                place++;
                index++;
            }
            string firstFinalAnimeList = "";
            string secondFinalAnimeList = "";
            string thirdFinalAnimeList = "";
            string al4 = "";
            string al5 = "";

            for (int i = 0; i <= 9; i++)
            {
                firstFinalAnimeList = firstFinalAnimeList + list[i];
            }
            for (int i = 10; i <= 19; i++)
            {
                secondFinalAnimeList = secondFinalAnimeList + list[i];
            }
            for (int i = 20; i <= 29; i++)
            {
                thirdFinalAnimeList = thirdFinalAnimeList + list[i];
            }
            for (int i = 30; i <= 39; i++)
            {
                al4 = al4 + list[i];
            }
            for (int i = 40; i <= 49; i++)
            {
                al5 = al5 + list[i];
            }


            EmbedBuilder firstEmb = new EmbedBuilder();
            firstEmb.WithTitle("Top Anime on MyAnimeList:")
                  .WithDescription(firstFinalAnimeList)
                  .WithColor(0x2e51a2);
            await ReplyAsync("", false, firstEmb.Build());

            EmbedBuilder secondEmb = new EmbedBuilder();
            secondEmb
                  .WithDescription(secondFinalAnimeList)
                  .WithColor(0x2e51a2);
            await ReplyAsync("", false, secondEmb.Build());

            EmbedBuilder thirdEmb = new EmbedBuilder();
            thirdEmb
                  .WithDescription(thirdFinalAnimeList)
                  .WithColor(0x2e51a2);
            await ReplyAsync("", false, thirdEmb.Build());

            EmbedBuilder Emb4 = new EmbedBuilder();
            Emb4
                  .WithDescription(al4)
                  .WithColor(0x2e51a2);
            await ReplyAsync("", false, Emb4.Build());

            EmbedBuilder Emb5 = new EmbedBuilder();
            Emb5
                  .WithDescription(al5)
                  .WithColor(0x2e51a2);
            await ReplyAsync("", false, Emb5.Build());
            
            
            return;
        }


        [Command("search")]
        public async Task AnimeSearch(string searchTask, int resultNum = 1)
        {
            IJikan jikan = new Jikan();
            await ReplyAsync("Results for " + searchTask + ":");

            AnimeSearchResult animeSearchResult = await jikan.SearchAnime(searchTask);
            int i = 0;
            foreach (var result in animeSearchResult.Results)
            {
                if(i == resultNum)
                {
                    return;
                }
                EmbedBuilder topEmb = new EmbedBuilder();
                topEmb.WithTitle(result.Title)
                      .WithUrl(result.URL)
                      .AddField("Description:\n",
                                result.Description +"\n\n" +
                                $"**Score: {result.Score}**\n")
                      .WithThumbnailUrl(result.ImageURL)
                      .WithDescription($"Currently Airing: {result.Airing}\n" + $"Started airing: {result.StartDate}\n" + $"Ended airing: {result.EndDate}")
                      .WithColor(0x2e51a2);


                await ReplyAsync("", false, topEmb.Build());
                i = i + 1;
            }
            return;
        }

        //Not working
        [Command("searchChar")]
        public async Task SearchChar(string searchTask, int resultNum = 1)
        {
            IJikan jikan = new Jikan();
            await ReplyAsync("Results for " + searchTask + ":");

            CharacterSearchResult charSearchResult = await jikan.SearchCharacter(searchTask);
            int i = 0;
            foreach (var result in charSearchResult.Results)
            {
                if (i == resultNum)
                {
                    return;
                }

                //Doesn't needed but nice to have
                /*string[] array = new string[result.AlternativeNames.Count];
                result.AlternativeNames.CopyTo(array, 0);
                int arrayLenght = array.Length;
                string alternativeNames = "";

                for(int n = 0; n <= arrayLenght - 1; n++)
                {
                    alternativeNames = alternativeNames + array[n] + "\n";
                }
                */
                string anime = "";
                foreach(MALSubItem subItem in result.Animeography)
                {
                    anime = anime + "[" + subItem.Name + "](" + subItem.Url + ")" + "\n";
                }

                if(anime.Length > 2048)
                {
                    EmbedBuilder topEmb = new EmbedBuilder();
                    topEmb.WithTitle(result.Name)
                          .WithUrl(result.URL)
                          .WithDescription("Anime:\n" +
                                    "Embedded anime list was to large! Too many anime <a:AYAYATriggered:623230429486645261>")
                          .WithThumbnailUrl(result.ImageURL)
                          .WithColor(0x2e51a2);
                    await ReplyAsync("", false, topEmb.Build());
                }
                else
                {

                    EmbedBuilder topEmb = new EmbedBuilder();
                    topEmb.WithTitle(result.Name)
                          .WithUrl(result.URL)
                          .WithDescription("Anime:\n" +
                                    anime)
                          .WithThumbnailUrl(result.ImageURL)
                          .WithColor(0x2e51a2);
                    await ReplyAsync("", false, topEmb.Build());
                }
                i = i + 1;
            }
            return;
        }

        //Not working
        [Command("topChars")]
        public async Task TopChars([Remainder]string str)
        {
            /*
            str = str.ToLower();
            IJikan jikan = new Jikan();
            CharactersTop charTop;
            int pageNum = Convert.ToInt32(str);
            charTop = await jikan.GetCharactersTop(pageNum);

            string charList = "";
            foreach (var topChar in charTop.Top)
            {
                charList = charList + topChar.Rank + " [" + topChar.Name + "](" + topChar.Url + ")\n";
            }

            string[] slicedList = charList.Split("\n");
            string finalCharList = "";

            for (int i = 0; i <= slicedList.Length - 1; i++)
            {
                finalCharList = finalCharList + slicedList[i] + "\n";
            }

            string firstString = "";
            string secondString = "";
            int lenght = finalCharList.Length;
            if(lenght > 2048)
            {
                firstString = finalCharList.Substring(0, 2048);
                secondString = finalCharList.Substring(2049, lenght - 2048);
            }


            EmbedBuilder topEmb = new EmbedBuilder();
            topEmb.WithTitle("Top Anime on MyAnimeList")
                  .WithDescription(firstString + secondString);
            await ReplyAsync("", false, topEmb.Build());
            return;
            */
        }
    }
}
