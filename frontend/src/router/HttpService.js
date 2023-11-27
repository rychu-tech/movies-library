import axios from "axios";
const BACKEND_URL = "http://localhost:8081/api";
export const httpService = {
  fetchMovies() {
    return axios.get(`${BACKEND_URL}/movies`);
  },
  deleteMovie(movieId) {
    return axios.delete(`${BACKEND_URL}/movies/${movieId}`);
  },
  restoreMovie(movieId) {
    return axios.patch(`${BACKEND_URL}/movies/${movieId}`);
  },
  importMovies() {
    return axios.get(`${BACKEND_URL}/movies/import`);
  },
  addMovie(formData) {
    console.log(formData);
    return axios.post(`${BACKEND_URL}/movies`, formData);
  },
  editMovie(formData) {
    return axios.put(`${BACKEND_URL}/movies/${formData.id}`, formData);
  },
};
