<template>
  <div>
    <button @click="showModal" class="btn btn-primary">
      <i class="bi bi-plus"></i> Add Movie
    </button>

    <div v-if="isModalVisible" class="custom-modal">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title">New Movie</h5>
          <button
            type="button"
            class="btn-close"
            @click="closeModal"
            aria-label="Close"
          ></button>
        </div>
        <div class="modal-body">
          <form @submit.prevent="handleSubmit">
            <div class="mb-3">
              <label for="title" class="form-label">Title</label>
              <input
                v-model="formData.title"
                type="text"
                class="form-control"
                required
                id="title"
                @blur="v$.formData.title.$touch"
              />
            </div>
            <div class="mb-3">
              <label for="releaseYear" class="form-label">Release Year</label>
              <input
                v-model="formData.releaseYear"
                type="number"
                class="form-control"
                required
                id="releaseYear"
                @blur="v$.formData.releaseYear.$touch"
              />
            </div>
            <button type="submit" class="btn btn-primary">Submit</button>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { handleFormValidation } from "../validation/FormValidator";
import { movieValidationRules } from "../validation/ValidationFunctions";
import { useVuelidate } from "@vuelidate/core";

export default {
  setup() {
    return { v$: useVuelidate() };
  },
  data() {
    return {
      isModalVisible: false,
      formData: {
        title: "",
        releaseYear: null,
      },
    };
  },
  validations() {
    return {
      formData: movieValidationRules,
    };
  },
  methods: {
    showModal() {
      this.isModalVisible = true;
    },
    closeModal() {
      this.isModalVisible = false;
      this.formData = {
        title: "",
        releaseYear: null,
      };
    },
    handleSubmit() {
      if (handleFormValidation(this.v$)) {
        return;
      }

      this.$emit("submitStore", this.formData);
      this.closeModal();
    },
  },
};
</script>

<style scoped>
.custom-modal {
  display: block;
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.5);
  z-index: 1050;
}

.modal-content {
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  background-color: #fff;
  padding: 20px;
  border-radius: 5px;
  width: 300px;
}
</style>
