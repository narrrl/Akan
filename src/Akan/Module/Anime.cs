using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Net;
using Nekos.Net;
using JikanDotNet;

namespace Akan.Module
{
    [Group("mal")]
    public class Anime : ModuleBase<SocketCommandContext>
    {
        [Command("season")]
        public async Task Season(int year, string seasonStr)
        {
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
                animeList = animeList + seasonEntry.Title + "\n";
            }

            string[] slicedList = animeList.Split("\n");
            string finalAnimeList = "";

            for(int i = 0; i <= slicedList.Length - 1 && finalAnimeList.Length < 1900; i++)
            {
                finalAnimeList = finalAnimeList + slicedList[i] + "\n";
            }

            seasonEmb.WithTitle("Season : " + season.SeasonYear + " " + season.SeasonName)
                     .WithDescription(finalAnimeList);

            await ReplyAsync("", false, seasonEmb.Build());

            
        }

        [Command("top")]
        public async Task TopAnime([Remainder]string str)
        {
            str = str.ToLower();
            IJikan jikan = new Jikan();
            AnimeTop topAnimeList;
            if (str.Length == 1)
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

            string animeList = "";
            foreach (var topAnime in topAnimeList.Top)
            {
                animeList = animeList + topAnime.Title + "\n";
            }

            string[] slicedList = animeList.Split("\n");
            string finalAnimeList = "";

            for (int i = 0; i <= slicedList.Length - 1 && finalAnimeList.Length < 1900; i++)
            {
                finalAnimeList = finalAnimeList + slicedList[i] + "\n";
            }


            EmbedBuilder topEmb = new EmbedBuilder();
            topEmb.WithTitle("Top Anime on MyAnimeList")
                  .WithDescription(finalAnimeList);
            await ReplyAsync("", false, topEmb.Build());
        }


        [Command("search")]
        public async Task AnimeSearch(string searchTask, int resultNum)
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
                      .WithDescription($"Currently Airing: {result.Airing}\n" + $"Started airing: {result.StartDate}\n" + $"Ended airing: {result.EndDate}");


                await ReplyAsync("", false, topEmb.Build());
                i = i + 1;
            }
        }
    }
}
