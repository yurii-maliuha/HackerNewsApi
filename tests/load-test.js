// k6 run load-test.js -e MY_HOSTNAME=http://localhost:5107
import http from 'k6/http';

export const options = {
    // A fixed number of VUs execute as many
    // iterations as possible for a specified amount of time.
    executor: 'constant-vus',

    // Virtual users count
    vus: 20,
    duration: '120s',
};

//export default function case_with_general_load() {
//    const MAX_COUNT_VALUE = 100;
//    const param = Math.floor(Math.random() * MAX_COUNT_VALUE);
//    const url = `${__ENV.MY_HOSTNAME}/api/best-stories?count=${param}`;
//    http.get(url);
//}


export default function case_with_many_cache_misses() {
    const MAX_COUNT_VALUE = 50;
    const param = 50 + Math.floor(Math.random() * MAX_COUNT_VALUE);
    const url = `${__ENV.MY_HOSTNAME}/api/best-stories?count=${param}`;
    //console.log(url)
    http.get(url);
}