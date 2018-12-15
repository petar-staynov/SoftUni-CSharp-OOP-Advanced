using System.Collections.Generic;
using FestivalManager.Entities;

namespace FestivalManager.Core.Controllers
{
    using System;
    using System.Linq;
    using Contracts;
    using Entities.Contracts;
    using Entities.Factories;
    using Entities.Factories.Contracts;

    public class FestivalController : IFestivalController
    {
        private const string TimeFormat = "mm\\:ss";
        private const string TimeFormatLong = "{0:2D}:{1:2D}";
        private const string TimeFormatThreeDimensional = "{0:3D}:{1:3D}";


        private ISetFactory SetFactory;
        private IInstrumentFactory InstrumentFactory;

        private List<ISet> Sets;
        private List<IPerformer> Performers;
        private List<IInstrument> Instruments;
        private List<ISong> Songs;


        private readonly IStage stage;

        public FestivalController(IStage stage)
        {
            this.stage = stage;
            this.SetFactory = new SetFactory();
            this.InstrumentFactory = new InstrumentFactory();

            this.Sets = new List<ISet>();
            this.Performers = new List<IPerformer>();
            this.Instruments = new List<IInstrument>();
            this.Songs = new List<ISong>();
        }

        public string ProduceReport()
        {
            var result = string.Empty;

            TimeSpan festivalTimeSpan = new TimeSpan(this.stage.Sets.Sum(s => s.ActualDuration.Ticks));

            string festivalLengthString =
                string.Format("{0:D2}:{1:D2}", (int) festivalTimeSpan.TotalMinutes, festivalTimeSpan.Seconds);

            result +=
                ($"Festival length: {festivalLengthString}\n");

            foreach (var set in this.stage.Sets)
            {
                TimeSpan setTimeSpan = set.ActualDuration;

                string setDurationString = 
                    string.Format("{0:D2}:{1:D2}", (int)setTimeSpan.TotalMinutes, setTimeSpan.Seconds);


                result += ($"--{set.Name} ({setDurationString}):\n");

                var performersOrderedDescendingByAge = set.Performers.OrderByDescending(p => p.Age);
                foreach (var performer in performersOrderedDescendingByAge)
                {
                    var instruments = string.Join(", ", performer.Instruments
                        .OrderByDescending(i => i.Wear));

                    result += ($"---{performer.Name} ({instruments})") + "\n";
                }

                if (!set.Songs.Any())
                    result += ("--No songs played") + "\n";
                else
                {
                    result += ("--Songs played:") + "\n";
                    foreach (var song in set.Songs)
                    {
                        result += ($"----{song.Name} ({song.Duration.ToString(TimeFormat)})") + "\n";
                    }
                }
            }

            return result.ToString().TrimEnd();
        }


        public string RegisterSet(string[] args)
        {
            string setName = args[0];
            string setType = args[1];
            ISet set = this.SetFactory.CreateSet(setName, setType);

            this.stage.AddSet(set);
            return $"Registered {setType} set";
        }

        public string SignUpPerformer(string[] args)
        {
            string name = args[0];
            int age = int.Parse(args[1]);
            string[] instruments = args.Skip(2).ToArray();


            IPerformer performer = new Performer(name, age);

            foreach (string instrument in instruments)
            {
                IInstrument instrumentObj = this.InstrumentFactory.CreateInstrument(instrument);
                performer.AddInstrument(instrumentObj);
            }

            this.stage.AddPerformer(performer);
            return $"Registered performer {name}";
        }

        public string RegisterSong(string[] args)
        {
            string songName = args[0];
            string songLength = args[1];
            TimeSpan songLenghTimeSpan =
                TimeSpan.ParseExact(songLength, TimeFormat, System.Globalization.CultureInfo.InvariantCulture);

            ISong song = new Song(songName, songLenghTimeSpan);
            this.stage.AddSong(song);

            return $"Registered song {songName} ({songLength})";
        }

        public string AddSongToSet(string[] args)
        {
            var songName = args[0];
            var setName = args[1];

            if (!this.stage.HasSet(setName))
            {
                throw new InvalidOperationException("Invalid set provided");
            }

            if (!this.stage.HasSong(songName))
            {
                throw new InvalidOperationException("Invalid song provided");
            }

            var set = this.stage.GetSet(setName);
            var song = this.stage.GetSong(songName);

            set.AddSong(song);

            return $"Added {song} to {set.Name}";
        }

        public string AddPerformerToSet(string[] args)
        {
            var performerName = args[0];
            var setName = args[1];

            if (!this.stage.HasPerformer(performerName))
            {
                throw new InvalidOperationException("Invalid performer provided");
            }

            if (!this.stage.HasSet(setName))
            {
                throw new InvalidOperationException("Invalid set provided");
            }

            var performer = this.stage.GetPerformer(performerName);
            var set = this.stage.GetSet(setName);

            set.AddPerformer(performer);

            return $"Added {performer.Name} to {set.Name}";
        }

        public string RepairInstruments(string[] args)
        {
            var instrumentsToRepair = this.stage.Performers
                .SelectMany(p => p.Instruments)
                .Where(i => i.Wear < 100)
                .ToArray();

            foreach (var instrument in instrumentsToRepair)
            {
                instrument.Repair();
            }

            return $"Repaired {instrumentsToRepair.Length} instruments";
        }
    }
}