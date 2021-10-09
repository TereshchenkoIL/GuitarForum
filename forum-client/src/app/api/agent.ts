import axios, {AxiosResponse} from "axios";
import Category from "../models/category";
import { PaginatedResult } from "../models/pagination";
import { Topic, TopicFormValues } from "../models/topic";


axios.defaults.baseURL = 'http://localhost:5000/api'

axios.interceptors.response.use(async response => {  

    const pagination = response.headers['pagination'];
    if(pagination){
        response.data = new PaginatedResult(response.data, JSON.parse(pagination))
        return response as AxiosResponse<PaginatedResult<any>>
    }
    return response;
})


const responseBody = <T>(response: AxiosResponse<T>) => response.data;


const requests = {
    get: <T>(url: string) => axios.get<T>(url).then(responseBody),
    post: <T>(url: string, body: {} ) => axios.post<T>(url, body).then(responseBody),
    put: <T>(url: string, body: {}) => axios.put<T>(url, body).then(responseBody),
    del: <T>(url: string) => axios.delete<T>(url).then(responseBody)
}

const Topics = {
    list: (params: URLSearchParams) => axios.get<PaginatedResult<Topic[]>>('/Topics/all', {params}).then(responseBody),
    listByCategory: (categoryId: string, params: URLSearchParams) => axios.get<PaginatedResult<Topic[]>>(`/Topics/category/${categoryId}`, {params}).then(responseBody),
    details: (id: string) => requests.get<Topic>(`/Topics/${id}`),
    create: (topic: TopicFormValues) => requests.post('/Topics', topic),
    update: (topic: TopicFormValues) => requests.put(`/Topics/${topic.id}`, topic),
    delete: (id: string) => requests.del<void>(`/Topics/${id}`),
    like: (id: string) => requests.post(`${id}/like`, {})

}


const Categories = {
    list: () => axios.get<Category[]>('/Categories'),
    create: (category: Category) => requests.post('/Categories', category),
    update: (category: Category) => requests.put(`/Categories`, category),
    delete: (id: string) => requests.del<void>(`/Categories/${id}`),
}


const agent = {
    Topics,
    Categories
};

export default agent;