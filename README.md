# NBAApp
To start the app open the .sln in Visual Studio and hit start debugging.

Features (as requested):
·         Date of event

·         Team names

·         Random images displayed as team logo

·         Score

·         Click on team names takes to page displaying team players details:

                    o    Name

                    o    Random images displayed as player's picture

                    o    Height & weight

                    o    Position on court
                    
There is also some logic to search google for the player/team image and display that in the tables, 
but google API blocks me after 5k requests.
A solution for this would be a caching service coupled with a throttling mechanism that will update a said number 
of pics per request refresh interval.
The application needs a "warm-up" upon starting to load data in its cache.

Known issues & questions:

1. I could not find a way to get current players per team.
    It appears that the API would not return played season for players.
    Downside: Players since 1980 all appear under a team. 
    Possible solution: Aggregate the data in a DB layer to better suit this app' needs.

2. Matches come in batches of max 100 / request.
    Downside:Such a behavior prevents ordering of the matches (show matches in chronological order).
    Possible solution: Same as 1.
    
3. Players can not be requested per team and search param does not work in API.
    Downside: All players must be retrieved at app start and loaded into cache.
    Possible solution: Same as 1. 
    
Given more time these issues could be easily fixed with a backend DB and a middle Cache.
Work time: ~9h
    

