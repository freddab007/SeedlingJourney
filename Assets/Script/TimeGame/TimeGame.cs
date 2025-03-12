using UnityEngine;

public class TimeGame : MonoBehaviour
{
    public enum Season
    {
        Spring,
        Summer,
        Autumn,
        Winter
    }
    public enum Day
    {
        Monday,
        Tuesday,
        Wednesday,
        Thurday,
        Friday,
        Saturday,
        Sunday,
    }

    public struct GameTime
    {
        public GameTime(Day _dayEnum, int _day, int _hour, int _minute, int _year)
        {
            weekDay = _dayEnum;
            day = _day;
            hours = _hour;
            minute = _minute;
            year = _year;
        }
        public Day weekDay;
        public int day;
        public int hours;
        public int minute;
        public int year;
    }

    string today;
    int maxDay = 28;

    int maxHours = 24;

    int maxMinutes = 60;
    Season season = Season.Spring;
    GameTime gameTime = new GameTime( Day.Monday, 1, 6, 0, 0);

    [SerializeField] float timerMinMax = 1;
    float timerMin = 0;

    public Season GetSeason()
    {
        return season;
    }

    // Start is called before the first frame update
    void Start()
    {
        UIGameManager.Instance.UpdateAllTime(gameTime);
        UIGameManager.Instance.UpdateSeasonText(season.ToString());

    }

    void UpdateTime()
    {
        timerMin += Time.deltaTime;
        if (timerMin >= timerMinMax)
        {
            timerMin = 0;
            AddMinute();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.GetPause())
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                //NewDay();
                AddMinute();
            }
            UpdateTime();
        }
        
    }

    public void AddMinute()
    {
        ++gameTime.minute;
        if (gameTime.minute >= maxMinutes)
        {
            gameTime.minute = 0;
            AddHour();
        }

        UIGameManager.Instance.UpdateAllTime(gameTime);
    }

    public void AddHour()
    {
        gameTime.hours++;
        if (gameTime.hours >= maxHours)
        {
            gameTime.hours = 0;
            AddDay();
        }
        UIGameManager.Instance.UpdateAllTime(gameTime);
    }

    static public string GetMinuteText(GameTime _gameTime)
    {
        string minuteText;
        if (_gameTime.minute < 10)
        {
            minuteText = "0" + _gameTime.minute;
        }
        else
        {
            minuteText = _gameTime.minute.ToString();
        }
        return minuteText;
    }

    static public string GetHourText(GameTime _gameTime)
    {
        string hourText;
        if (_gameTime.hours < 10)
        {
            hourText = "0" + _gameTime.hours;
        }
        else
        {
            hourText = _gameTime.hours.ToString();
        }
        return hourText;
    }

    public void NewDay()
    {
        PlantManager.Instance.LaunchGrow();
        PlantManager.Instance.LaunchDeath();
        MapTileManager.instance.WetToDry();
        if (gameTime.hours >= 2 && gameTime.hours <= maxHours)
        {
            AddDay();
        }
        gameTime.hours = 6;
        gameTime.minute = 0;
    }

    public void AddDay()
    {
        ++gameTime.day;
        if (gameTime.day > maxDay)
        {
            gameTime.day = 1;
            ChangeSeason();
        }
        gameTime.weekDay = (Day)((gameTime.day - 1) % 7);
        today = gameTime.weekDay.ToString().Remove(3);
        UIGameManager.Instance.UpdateAllTime(gameTime);

    }

    public void ChangeSeason()
    {
        ++season;
        if (season > Season.Winter)
        {
            season = Season.Spring;
            ChangeYear();
        }
        UIGameManager.Instance.UpdateSeasonText(season.ToString());
    }

    public void ChangeYear()
    {
        ++gameTime.year;
        UIGameManager.Instance.UpdateAllTime(gameTime);

    }
}
