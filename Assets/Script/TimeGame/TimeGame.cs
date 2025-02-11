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

    int year = 1;
    Day weekDay = Day.Monday;
    string today;
    int day = 1;
    int maxDay = 28;

    int hours = 6;
    int maxHours = 24;

    int minute = 0;
    int maxMinutes = 60;
    Season season = Season.Spring;


    public Season GetSeason()
    {
        return season;
    }

    // Start is called before the first frame update
    void Start()
    {
        UIGameManager.Instance.UpdateDayText("Mon. " + day.ToString());
        UIGameManager.Instance.UpdateYearText(year.ToString());

        UIGameManager.Instance.UpdateSeasonText(season.ToString());

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            AddDay();
        }
    }

    public void AddMinute()
    {
        ++minute;
        if (minute >= maxMinutes)
        {
            minute = 0;
            AddHour();
        }

        UIGameManager.Instance.UpdateTimeText(GetHourText() + ":" + GetMinuteText());
    }

    public void AddHour()
    {
        hours++;
        if (hours >= maxHours)
        {
            hours = 0;
            AddDay();
        }
        UIGameManager.Instance.UpdateTimeText(GetHourText() + ":" + GetMinuteText());
    }

    string GetMinuteText()
    {
        string minuteText;
        if (minute < 10)
        {
            minuteText = "0" + minute;
        }
        else
        {
            minuteText = minute.ToString();
        }
        return minuteText;
    }

    string GetHourText()
    {
        string hourText;
        if (hours < 10)
        {
            hourText = "0" + hours;
        }
        else
        {
            hourText = hours.ToString();
        }
        return hourText;
    }

    public void NewDay()
    {
        PlantManager.Instance.LaunchGrow();
        PlantManager.Instance.LaunchDeath();
        MapManager.instance.WetToDry();
        if (hours <= 2)
        {
            AddDay();
        }
        hours = 6;
        minute = 0;
    }

    public void AddDay()
    {
        ++day;
        if (day > maxDay)
        {
            day = 1;
            ChangeSeason();
        }
        weekDay = (Day)((day - 1) % 7);
        today = weekDay.ToString().Remove(3);
        UIGameManager.Instance.UpdateDayText(today + ". " + day.ToString());

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
        ++year;
        UIGameManager.Instance.UpdateYearText(year.ToString());

    }
}
