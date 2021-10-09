import axios, {AxiosResponse} from "axios";
import { PaginatedResult } from "../models/pagination";
import { Topic, TopicFormValues } from "../models/topic";

axios.defaults.baseURL = 'http:/localhost:5001/api'

const responseBody = <T>(response: AxiosResponse<T>) => response.data;

const requests = {
    get: <T>(url: string) => axios.get<T>(url).then(responseBody),
    post: (url: string, body: {} ) => axios.post(url, body).then(responseBody),
    put: (url: string, body: {}) => axios.put(url, body).then(responseBody),
    del: <T>(url: string) => axios.delete<T>(url).then(responseBody)
}

const Topics = {
    list: (params: URLSearchParams) => axios.get<PaginatedResult<Topic[]>>('/topics/all', {params}).then(responseBody),
    listByCategory: (categoryId: string, params: URLSearchParams) => axios.get<PaginatedResult<Topic[]>>(`/topics/category/${categoryId}`, {params}).then(responseBody),
    details: (id: string) => requests.get<Topic>(`/topics/${id}`),
    create: (topic: TopicFormValues) => requests.post('/topics', topic),
    update: (topic: TopicFormValues) => requests.put(`/topics/${topic.id}`, topic),
    delete: (id: string) => requests.del<void>(`/topics/${id}`),
    like: (id: string) => requests.post(`${id}/like`, {})

}


const agent = {
    Topics
};

export default agent;