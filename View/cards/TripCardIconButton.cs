using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atestat1.View.cards
{
    internal class TripCardIconButton : IconButton
    {
        TripCard trip;
        public TripCard TRIP { get => this.trip; }

        public TripCardIconButton(TripCard trip)
        {
            this.trip = trip;
        }
    }
}
