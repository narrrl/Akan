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
        public async Task Season( int page = 1, int year = 0, string seasonStr = null)
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
            EmbedBuilder title = new EmbedBuilder();
            int entrysCount = season.SeasonEntries.Count;
            string[] animeList = new string[entrysCount];
            int index = 0;
            title.WithTitle("Season: " + season.SeasonYear + " " + season.SeasonName)
                .WithColor(0x2e51a2);
            await ReplyAsync("", false, title.Build());
            foreach (var seasonEntry in season.SeasonEntries)
            {
                animeList[index] = "[" + seasonEntry.Title + "](" + seasonEntry.URL + ")\n" + "Ep: [" + seasonEntry.Episodes + "]  Started: " + Methods.GetStringTime(seasonEntry.AiringStart);
                index++;
            }
            int currEmb = 0;
            string animeTemp = "";
            for(int i = 0; i <= animeList.Length - 1; i++)
            {
                animeTemp = animeTemp + animeList[i] + "\n";
                if(animeTemp.Length > 1700)
                {
                    currEmb++;
                    if(currEmb == page)
                    {
                        seasonEmb
                     .WithDescription(animeTemp)
                     .WithColor(0x2e51a2);
                        await ReplyAsync("", false, seasonEmb.Build());
                    }
                    animeTemp = "";
                }
            }
            return;
        }

        [Command("today")]
        public async Task todayAnime(int page = 1)
        {
            int year = yearCurrently;
            string seasonStr = seasonCurrently;
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

            string[] weekDayCollection = new string[season.SeasonEntries.Count];
            int index = 0;
            foreach(var anime in season.SeasonEntries)
            {
                weekDayCollection[index] = Methods.GetWeekDay(anime.AiringStart);
                index++;
            }
            index = 0;
            string today = DateTime.Now.DayOfWeek.ToString();

            string[] animeList = new string[season.SeasonEntries.Count];
            foreach (var anime in season.SeasonEntries)
            {
                if (weekDayCollection[index].Equals(today))
                {
                    animeList[index] = "[" + anime.Title + "](" + anime.URL + ")\n" + "Ep: [" + anime.Episodes + "]  Today at " + Methods.getTime(anime.AiringStart);
                }
                else
                {
                    animeList[index] = "";
                }
                index++;
            }
            EmbedBuilder seasonEmb = new EmbedBuilder();
            EmbedBuilder title = new EmbedBuilder();
            title.WithTitle("Anime today:")
                .WithColor(0x2e51a2);
            int currEmb = 0;
            string animeTemp = "";
            for (int i = 0; i <= animeList.Length - 1; i++)
            {
                if (!animeList[i].Equals(""))
                {
                    animeTemp = animeTemp + animeList[i] + "\n";
                }
                if (animeTemp.Length > 1700)
                {
                    currEmb++;
                    if (currEmb == page)
                    {
                        seasonEmb
                     .WithDescription(animeTemp)
                     .WithColor(0x2e51a2);
                        await ReplyAsync("", false, seasonEmb.Build());
                    }
                    animeTemp = "";
                }
            }
            return;


        }

        [Command("nextSeason")]
        public async Task SeasonNext(int page = 1)
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
            EmbedBuilder title = new EmbedBuilder();
            int entrysCount = season.SeasonEntries.Count;
            string[] animeList = new string[entrysCount];
            int index = 0;
            title.WithTitle("Season: " + season.SeasonYear + " " + season.SeasonName)
                .WithColor(0x2e51a2);
            await ReplyAsync("", false, title.Build());
            foreach (var seasonEntry in season.SeasonEntries)
            {
                animeList[index] = "[" + seasonEntry.Title + "](" + seasonEntry.URL + ")\n" + "Ep: [" + seasonEntry.Episodes + "]  Starting: " + Methods.GetStringTime(seasonEntry.AiringStart);
                index++;
            }
            int currEmb = 0;
            string animeTemp = "";
            for (int i = 0; i <= animeList.Length - 1; i++)
            {
                animeTemp = animeTemp + animeList[i] + "\n";
                if (animeTemp.Length > 1700)
                {
                    currEmb++;
                    if (currEmb == page)
                    {
                        seasonEmb
                     .WithDescription(animeTemp)
                     .WithColor(0x2e51a2);
                        await ReplyAsync("", false, seasonEmb.Build());
                    }
                    animeTemp = "";
                }
            }
            return;
        }

        [Command("top")]
        public async Task TopAnime([Remainder]string str = "1")
        {
            EmbedBuilder title = new EmbedBuilder();
            str = str.ToLower();
            IJikan jikan = new Jikan();
            AnimeTop topAnimeList;
            EmbedBuilder seasonEmb = new EmbedBuilder();
            int entrysCount;
            string[] animeList;
            int index = 0;
            if (str.Length <= 5)
            {
                int pageNum = Convert.ToInt32(str);
                topAnimeList = await jikan.GetAnimeTop(pageNum);
                title.WithTitle("Top Anime Page " + pageNum)
                .WithColor(0x2e51a2);
                await ReplyAsync("", false, title.Build());
                entrysCount = topAnimeList.Top.Count;
                animeList = new string[entrysCount];
                foreach (var topAnime in topAnimeList.Top)
                {
                    animeList[index] = "[" + topAnime.Title + "](" + topAnime.Url + ")\n" + "Ep: [" + topAnime.Episodes + "]  Score: [" + topAnime.Score + "]";
                    index++;
                }
            }
            else if(str.Equals("airing"))
            {

                topAnimeList = await jikan.GetAnimeTop(TopAnimeExtension.TopAiring);
                title.WithTitle("Top Airing Anime")
                .WithColor(0x2e51a2);
                await ReplyAsync("", false, title.Build());
                entrysCount = topAnimeList.Top.Count;
                animeList = new string[entrysCount];
                foreach (var topAnime in topAnimeList.Top)
                {
                    animeList[index] = "[" + topAnime.Title + "](" + topAnime.Url + ")\n" + "Ep: [" + topAnime.Episodes + "]  Members: [" + topAnime.Members + "]  Score: [" + topAnime.Score + "]";
                    index++;
                }
            }
            else if (str.Equals("upcoming"))
            {
                topAnimeList = await jikan.GetAnimeTop(TopAnimeExtension.TopUpcoming);
                title.WithTitle("Top Upcoming Anime")
                .WithColor(0x2e51a2);
                await ReplyAsync("", false, title.Build());
                entrysCount = topAnimeList.Top.Count;
                animeList = new string[entrysCount];
                foreach (var topAnime in topAnimeList.Top)
                {
                    animeList[index] = "[" + topAnime.Title + "](" + topAnime.Url + ")\n" + "Ep: [" + topAnime.Episodes + "]  Members: [" + topAnime.Members + "]";
                    index++;
                }
            }
            else
            {
                await ReplyAsync("Error!");
                return;
            }
            string animeTemp = "";
            for (int i = 0; i <= animeList.Length - 1; i++)
            {
                animeTemp = animeTemp + animeList[i] + "\n";
                if (animeTemp.Length > 1700)
                {
                    seasonEmb
                     .WithDescription(animeTemp)
                     .WithColor(0x2e51a2);
                        await ReplyAsync("", false, seasonEmb.Build());
                    animeTemp = "";
                }
            }
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
                                $"**Score: {result.Score} Rated:{result.Rated}**\n")
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
            /*
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
            */
            await ReplyAsync("Doesn't work currently");
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
            */
            await ReplyAsync("Doesn't work currently");
            return;
        }
    }
}
