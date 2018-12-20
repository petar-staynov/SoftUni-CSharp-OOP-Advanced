using Travel.Entities.Airplanes;
using Travel.Entities.Airplanes.Contracts;
using Travel.Entities.Items;
using Travel.Entities.Items.Contracts;

namespace Travel.Core.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Entities;
    using Entities.Contracts;
    using Entities.Factories;
    using Entities.Factories.Contracts;

    public class AirportController : IAirportController
    {
        private const int BagValueConfiscationThreshold = 3000;

        private IAirport airport;

        private IAirplaneFactory airplaneFactory = null;
        private IItemFactory itemFactory;

        public AirportController(IAirport airport)
        {
            this.airport = airport;
            this.airplaneFactory = new AirplaneFactory();
            this.itemFactory = new ItemFactory();
        }

        public string RegisterPassenger(string username)
        {
            if (this.airport.GetPassenger(username) != null)
            {
                throw new InvalidOperationException($"Passenger {username} already registered!");
            }

            //TODO Factory
            var passenger = new Passenger(username);

            this.airport.AddPassenger(passenger);
            return $"Registered {passenger.Username}";
        }

        public string RegisterTrip(string source, string destination, string planeType)
        {
            IAirplane plane = airplaneFactory.CreateAirplane(planeType);
            var trip = new Trip(source, destination, plane);
            this.airport.AddTrip(trip);
            return $"Registered trip {trip.Id}";
        }

        public string RegisterBag(string username, IEnumerable<string> bagItems)
        {
            var passenger = this.airport.GetPassenger(username);

            List<IItem> items = new List<IItem>();
            foreach (string bagItem in bagItems)
            {
                //TODO factory
                IItem item = itemFactory.CreateItem(bagItem);
                items.Add(item);
            }

            //TODO factory
            var bag = new Bag(passenger, items);

            passenger.Bags.Add(bag);
            return $"Registered bag with {string.Join(", ", bagItems)} for {username}";
        }


        public string CheckIn(string username, string tripId, IEnumerable<int> bagIndices)
        {
            /*Gets a passenger with the provided username and a trip with the provided id.
               If the passenger has already checked in (is already in any trips’ airplanes), throw an InvalidOperationException with the message "{username} is already checked in!". 
               Then, the command checks in all the passenger bags with that index.
               Checking in works like this:
               The bag with that index gets removed from the passenger’s bags. Then, depending on whether the bag should be confiscated, one of the following things happens:
               If it should be confiscated (if the total value of the bag is over $3000), the bag is added to the airport’s confiscated bags. If not, the bag gets added to the airport’s checked bags. Any other bags, whose indices are not listed in the command input are left with the passenger (and eventually board the plane along with the passenger).
               After checking in any bags, the passenger is added to the trip.
               The command returns "Checked in {username} with {bagsToCheckInCount-confiscatedBagsCount}/{bagsToCheckInCount} checked in bags".
            */

            var passenger = this.airport.GetPassenger(username);
            var trip = this.airport.GetTrip(tripId);

            bool isCheckedIn = trip.Airplane.Passengers.Any(p => p.Username == username);

            if (isCheckedIn)
            {
                throw new InvalidOperationException($"{username} is already checked in!");
            }

            //TODO maybe fix this
            var confiscatedBags = CheckInBags(passenger, bagIndices);
            trip.Airplane.AddPassenger(passenger);

            return
                $"Checked in {passenger.Username} with {bagIndices.Count() - confiscatedBags}/{bagIndices.Count()} checked in bags";
        }

        private int CheckInBags(IPassenger passenger, IEnumerable<int> bagsToCheckIn)
        {
            var bags = passenger.Bags;

            var confiscatedBagCount = 0;
            foreach (int bagId in bagsToCheckIn)
            {
                var currentBag = bags[bagId];
                bags.RemoveAt(bagId);

                if (ShouldConfiscate(currentBag))
                {
                    airport.AddConfiscatedBag(currentBag);
                    confiscatedBagCount++;
                }
                else
                {
                    this.airport.AddCheckedBag(currentBag);
                }
            }

            return confiscatedBagCount;
        }

        private static bool ShouldConfiscate(IBag bag)
        {
            var luggageValue = 0;

            for (int i = 0; i < bag.Items.Count; i++)
            {
                luggageValue += bag.Items.ToArray()[i].Value;
            }

            var shouldConfiscate = luggageValue > BagValueConfiscationThreshold;
            return shouldConfiscate;
        }
    }
}