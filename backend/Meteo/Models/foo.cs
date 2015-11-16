public JsonCard oneDay(List<JsonCard> allDayCards)
        {
            JsonCard day = new JsonCard();
            day.Date = allDayCards[0].Date;
            day.Description = allDayCards[0].Description;
            day.WindDirection = allDayCards[0].WindDirection;
            int maxWindDirection = 1;
            int maxDescription = 1;
            int curMax = 0;
            foreach (var card in allDayCards)
            {
                day.Humidity += card.Humidity/8;
                day.Temperature += card.Temperature/8;
                foreach (var item in allDayCards)
                {
                    curMax = 0;
                    if (item.WindDirection == card.WindDirection)
                        curMax++;
                }
                if (curMax > maxWindDirection)
                {
                    day.WindDirection = card.WindDirection;
                    maxWindDirection = curMax;
                }
                foreach (var item in allDayCards)
                {
                    curMax = 0;
                    if (item.Description == card.Description)
                        curMax++;
                }
                if (curMax > maxDescription)
                {
                    day.Description = card.Description;
                    maxDescription = curMax;
                }
            }
            return day;
        }