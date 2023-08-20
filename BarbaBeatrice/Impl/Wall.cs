using BarbaBeatrice.API;

namespace BarbaBeatrice.Impl{
    public class Wall : IWall{
        private double _height;

        public Wall(double size){
            _height = size;
        }
        public Wall(): this(0.0){}

        public double GetPosition() => _height;

        public void GoDown(double size){
            _height += size;
        }

        public new string ToString() => "Height wall is:" + _height;
    }
}