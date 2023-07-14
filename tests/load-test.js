// k6 run load-test.js
import http from 'k6/http';

//export const options = {
//    vus: 20,
//    duration: '120s',
//};
export const options = {
    vus: 10,
    duration: '60s',
};

export default function case_with_almost_cached_values_usage() {
    const MAX_COUNT_VALUE = 60;
    const param = Math.floor(Math.random() * MAX_COUNT_VALUE);
    const url = `http://localhost:53521/api/best-stories?count=${param}`;
    //console.log(url)
    http.get(url);
}

//export default function case_with_general_load() {
//    const MAX_COUNT_VALUE = 100;
//    const param = Math.floor(Math.random() * MAX_COUNT_VALUE);
//    const url = `http://localhost:53521/api/best-stories?count=${param}`;
//    //console.log(url)
//    http.get(url);
//}

//export default function case_with_huge_load() {
//    const MAX_COUNT_VALUE = 50;
//    const param = 50 + Math.floor(Math.random() * MAX_COUNT_VALUE);
//    const url = `http://localhost:53521/api/best-stories?count=${param}`;
//    //console.log(url)
//    http.get(url);
//}