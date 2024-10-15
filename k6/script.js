import http from 'k6/http';
import { sleep, check } from 'k6';
import { Counter } from 'k6/metrics';

export const requests = new Counter('http_reqs');

export const options = {
    scenarios: {
        sampleTest: {
            executor: 'constant-arrival-rate',
            exec: 'sampleTest',
            duration: '60s',
            rate: 600,
            timeUnit: `1s`,
            preAllocatedVUs: 4,
            maxVUs: 10,
        }
    }
};

export function sampleTest() {
    let res = http.get("http://brazil-cities-api/api/Cities");

    let checkRes = check(res, {
        'status is 200': (r) => r.status === 200,
    });
}

//docker build . -t k6
//docker run --network=brazil-cities_brazil-cities-network --rm k6 run script.js