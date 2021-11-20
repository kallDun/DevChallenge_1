Service for calculating average cost of financial interactions.
path -> 'api/prices/'

'path/average' takes arguments string:"portfolio", string:"owner", string:"instrument", DateTime:"date"
request example: '/api/prices/average?portfolio={portfolio}&owner={owner}&instrument={instrument}&datetime={datetime}'
request '/path/average' performs the task with or without one or more arguments

'path/benchmark' takes arguments string:"portfolio", DateTime:"date" and finds benchmark in one time interval (10000s)
request example: 'api/prices/benchmark?portfolio=Fannie Mae&date=15/03/2018 17:34:50'

'path/aggregate' takes arguments string:"portfolio", DateTime:"startdate", DateTime:"enddate", int:"intervals" and divide timeline 
between startdate and enddate into 'intervals' groupsand find average benchmark in every group
request example: 'api/prices/aggregate?portfolio=Fannie Mae&startdate=06/10/2018 00:00:00&enddate=13/10/2018 00:00:00&intervals=7'

service may return 'not found 404' if nothing find or 'bad request 400' if something gone wrong
