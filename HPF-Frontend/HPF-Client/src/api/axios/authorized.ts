import axios from 'axios';

const authorizedAxios = axios.create({
  baseURL: import.meta.env.VITE_API_BASE_URL+'api/' || 'https://api.example.com',
});

// Automatically inject JWT token from localStorage
authorizedAxios.interceptors.request.use((config) => {
  const token = localStorage.getItem('token');
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

authorizedAxios.interceptors.response.use(
  response => response,
  error => {
    if (error.response?.status === 401) {
      localStorage.removeItem('token');
      window.location.href = '/login';
    }
    return Promise.reject(error);
  }
);

export default authorizedAxios;
