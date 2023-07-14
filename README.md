# Hacker News API

## Set-up
The application can be run from your editor or using the following command:
* make sure you are in the solution folder
* run the following command
	```
		dotnet run --project ./src/HackerNews.Api/HackerNews.Api.csproj
	```
* API should be available on this URL http://localhost:5107/index.html

## Load Testing
[K6 library](https://k6.io/docs/) was use for load testing. The load test script can be found at `test/load-testing.js` file. To be able to trigger load test, make sure
* finished installation step [here](https://k6.io/docs/get-started/installation/)
* navigate to `test/load-testing.js` and trigger the command:
	```
		k6 run load-test.js -e MY_HOSTNAME=http://localhost:5107
	```

You can find the results of load testing here - https://github.com/yurii-maliuha/HackerNewsApi/wiki/Load-test-results


## Possible Enhancements
* Consider using TLP Dataflow
* Replace in-memory cache with distributed cache like Redis
* Fix possible race condition on value setting to cache (_CacheBase.GetValue_)