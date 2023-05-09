namespace CSharpTest
{
    public class HitPoint
    {
        private readonly int _value;
        public int Value => _value;

        public HitPoint(int value)
        {
            if (value < 0)
            {
                throw new ArgumentException("체력은 0 이상이어야 합니다.");
            }
            _value = value;
        }

        public HitPoint Subtract(int amount)
        {
            int newValue = _value - amount;
            return new HitPoint(newValue < 0 ? 0 : newValue);
        }

        public bool Same(HitPoint other)
        {
            return this._value == other._value;
        }
    }

    public class MagicPoint
    {
        private readonly int _value;
        public int Value => _value;

        public MagicPoint(int value)
        {
            if (value < 0)
            {
                throw new ArgumentException("마나는 0 이상이어야 합니다.");
            }
            _value = value;
        }

        public MagicPoint Subtract(int amount)
        {
            int newValue = _value - amount;
            return new MagicPoint(newValue < 0 ? 0 : newValue);
        }

        public bool Same(MagicPoint other)
        {
            return this._value == other._value;
        }
    }
    public class Player
    {
        private HitPoint _hp;
        private MagicPoint _mp;

        public HitPoint HP
        {
            get { return _hp; }
            private set { _hp = value; }
        }
        public MagicPoint MP
        {
            get { return _mp; }
            private set { _mp = value; }
        }

        public Player(HitPoint hp, MagicPoint mp)
        {
            HP = hp;
            MP = mp;
        }

        public void Damage(HitPoint damage, MagicPoint mpCost)
        {
            if (!HP.Same(new HitPoint(0)))
            {
                HP = HP.Subtract(damage.Value);
            }

            MP = MP.Subtract(mpCost.Value);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // 체력과 마나 생성
            var hp1 = new HitPoint(100);
            var mp1 = new MagicPoint(50);
            var hp2 = new HitPoint(150);
            var mp2 = new MagicPoint(100);

            // 플레이어 생성
            var player1 = new Player(hp1, mp1);
            var player2 = new Player(hp2, mp2);

            // 게임 시작
            Console.WriteLine("게임 시작!");
            Console.WriteLine("플레이어 1 체력: {0}, 마나: {1}", player1.HP.Value, player1.MP.Value);
            Console.WriteLine("플레이어 2 체력: {0}, 마나: {1}", player2.HP.Value, player2.MP.Value);

            // 공격
            Console.WriteLine("플레이어 1이 플레이어 2를 공격!");
            var damage = new HitPoint(10);
            var mpCost = new MagicPoint(5);
            player2.Damage(damage, mpCost);

            // 결과 출력
            Console.WriteLine("플레이어 1 체력: {0}, 마나: {1}", player1.HP.Value, player1.MP.Value);
            Console.WriteLine("플레이어 2 체력: {0}, 마나: {1}", player2.HP.Value, player2.MP.Value);
        }
    }

}