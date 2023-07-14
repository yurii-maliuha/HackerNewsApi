// k6 run load-test.js
import http from 'k6/http';

export const options = {
    vus: 20,
    duration: '120s',
};

//export const options = {
//    vus: 10,
//    duration: '60s',
//};

export default function () {
    const MAX_COUNT_VALUE = 30;
    const param = Math.floor(Math.random() * MAX_COUNT_VALUE);
    const url = `http://localhost:53521/api/best-stories?count=${param}`;
    //console.log(url)
    http.get(url);
}