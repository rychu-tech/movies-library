<template>
  <div class="container mt-5 text-center">
    <div class="d-flex justify-content-between mb-3">
      <div>
        <button @click="importMovies" class="btn btn-primary">
          Import Movies
        </button>
      </div>
      <h1 class="mb-0">Movie Page</h1>
      <div>
        <add-movie-form @submit="addMovie" />
      </div>
    </div>

    <div class="d-flex justify-content-center">
      <div class="table-container">
        <table class="table table-bordered mt-3">
          <thead>
            <tr>
              <th scope="col">Title</th>
              <th scope="col">Release Year</th>
              <th scope="col">Actions</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="movie in movieList" :key="movie.id" :class="{ 'text-decoration-line-through': movie.active === false }">
              <td>{{ movie.title }}</td>
              <td>{{ movie.releaseYear }}</td>
              <td v-if="movie.active">
                <button @click="deleteMovie(movie.id)" class="btn btn-danger">
                  <i class="bi bi-trash"></i> Delete
                </button>
              </td>
              <td v-else>
                <span></span>
              </td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
  </div>
</template>

<script>
import axios from 'axios';
import AddMovieForm from './AddMovieForm.vue';

export default {
  components: {
    'add-movie-form': AddMovieForm,
  },
  data() {
    return {
      movieList: [],
    };
  },
  mounted() {
    this.fetchMovies();
  },
  methods: {
    fetchMovies() {
      axios.get(`${this.BACKEND_URL}/movies`)
        .then(response => {
          this.movieList = response.data;
        })
        .catch(error => {
          console.error('Error fetching movies:', error);
        });
    },
    deleteMovie(movieId) {
      axios.delete(`${this.BACKEND_URL}/movies/${movieId}`)
        .then(response => {
          this.fetchMovies();
        })
        .catch(error => {
          console.error('Error deleting movie:', error);
        });
    },
    importMovies() {
      axios.get(`${this.BACKEND_URL}/movies/import`)
        .then(response => {
          this.fetchMovies();
          if (response.data.length === 0) {
            window.alert('There are currently no movies available.');
          }
        })
        .catch(error => {
          console.error('Error importing movies:', error);
        });
    },
    addMovie(formData) {
      axios.post(`${this.BACKEND_URL}/movies`, formData)
        .then(response => {
          this.fetchMovies();
        })
        .catch(error => {
          console.error('Error adding movie:', error);
        });
    },
  },
};
</script>

<style>
.text-decoration-line-through {
  text-decoration: line-through;
}

.table-container {
  width: 80%;
  max-height: 500px;
  overflow-y: auto;
}

.table-container thead th {
  position: sticky;
  top: 0;
  background-color: white;
  z-index: 1;
}

</style>
