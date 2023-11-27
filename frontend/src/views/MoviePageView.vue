<template>
  <div class="container mt-5 text-center">
    <div class="d-flex justify-content-between mb-3">
      <div>
        <button @click="importMovies" class="btn btn-success">
          <i class="bi bi-download"></i> Import Movies
        </button>
      </div>
      <h1 class="mb-0">Movie Page</h1>
      <div>
        <add-movie-form @submitStore="addMovie" />
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
            <tr
              v-for="movie in movieList"
              :key="movie.id"
              :class="{
                'text-decoration-line-through': movie.active === false,
              }"
            >
              <td>{{ movie.title }}</td>
              <td>{{ movie.releaseYear }}</td>
              <td v-if="movie.active">
                <div class="d-flex justify-content-center">
                  <edit-movie-form
                    :movieData="movie"
                    @submitEdit="editMovie"
                    class="me-2"
                  />
                  <delete-confirmation-modal
                    :movieId="movie.id"
                    @submitDelete="deleteMovie"
                  />
                </div>
              </td>
              <td v-else>
                <button @click="restoreMovie(movie.id)" class="btn btn-success">
                  <i class="bi bi-arrow-repeat"></i> Restore
                </button>
              </td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
  </div>
</template>

<script>
import AddMovieForm from "./AddMovieForm.vue";
import EditMovieForm from "./EditMovieForm.vue";
import DeleteConfirmationModal from "../components/DeleteConfirmationModal.vue";
import { httpService } from "../router/HttpService";
import { useVuelidate } from "@vuelidate/core";

export default {
  components: {
    "add-movie-form": AddMovieForm,
    "edit-movie-form": EditMovieForm,
    "delete-confirmation-modal": DeleteConfirmationModal,
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
    async fetchMovies() {
      try {
        const response = await httpService.fetchMovies();
        this.movieList = response.data;
      } catch (error) {
        window.alert("Error fetching movies!");
      }
    },
    async deleteMovie(movieId) {
      try {
        const response = await httpService.deleteMovie(movieId);
        this.fetchMovies();
      } catch (error) {
        window.alert("Error deleting movie!");
      }
    },
    async restoreMovie(movieId) {
      try {
        const response = await httpService.restoreMovie(movieId);
        this.fetchMovies();
      } catch (error) {
        window.alert("Error restoring movie!");
      }
    },
    async importMovies(movieId) {
      try {
        const response = await httpService.importMovies();
        if (response.data.length === 0) {
          window.alert("There are currently no movies available.");
        }
        this.fetchMovies();
      } catch (error) {
        window.alert("Error importing movies!");
      }
    },
    async addMovie(formData) {
      try {
        const response = await httpService.addMovie(formData);
        this.fetchMovies();
      } catch (error) {
        window.alert("Error adding movie!");
      }
    },
    async editMovie(formData) {
      try {
        const response = await httpService.editMovie(formData);
        this.fetchMovies();
      } catch (error) {
        window.alert("Error editting movie!");
      }
    },
  },
};
</script>

<style>
.text-decoration-line-through {
  text-decoration: line-through;
}

.table-container {
  width: 100vw;
  max-height: 500px;
  overflow-y: auto;
}

.table-container thead th {
  position: sticky;
  top: 0;
  background-color: lightgray;
  z-index: 1;
}
</style>
