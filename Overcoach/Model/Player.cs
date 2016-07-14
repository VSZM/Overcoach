using System;

namespace Overcoach.Model
{
    class Player : IEquatable<Player>
    {
        public string Name { get; set; }
        public Hero hero { get; set; }
        public Side side { get; set; }

        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = 17;
                // Suitable nullity checks etc, of course :)
                hash = hash * 23 + Name == null ? 0 : Name.GetHashCode();
                hash = hash * 23 + hero.GetHashCode();
                hash = hash * 23 + side.GetHashCode();
                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            return Equals((Player)obj);
        }

        public virtual bool Equals(Player other)
        {
            if (other == null) { return false; }
            if (ReferenceEquals(this, other)) { return true; }
            return Name == other.Name && hero == other.hero && side == other.side;
        }

        public static bool operator ==(Player player1, Player player2)
        {
            if (ReferenceEquals(player1, player2)) { return true; }
            if ((object)player1 == null || (object)player2 == null) { return false; }
            return player1.Equals(player2);
        }

        public static bool operator !=(Player player1, Player player2)
        {
            return !(player1 == player2);
        }
    }
}
