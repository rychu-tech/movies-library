<template>
  <div>
    <button @click="showModal" class="btn btn-primary">Add Movie</button>

    <div v-if="isModalVisible" class="custom-modal">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title">New Movie</h5>
          <button type="button" class="btn-close" @click="closeModal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
          <form @submit.prevent="handleSubmit">
            <div class="mb-3">
              <label for="title" class="form-label">Title</label>
              <input v-model="formData.title" type="text" class="form-control" required id="title" @blur="v$.formData.title.$touch">
            </div>
            <div class="mb-3">
              <label for="releaseYear" class="form-label">Release Year</label>
              <input v-model="formData.releaseYear" type="number" class="form-control" required id="releaseYear" @blur="v$.formData.releaseYear.$touch">
            </div>
            <button type="submit" class="btn btn-primary">Submit</button>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { required, maxLength, between } from '@vuelidate/validators';
import { useVuelidate } from '@vuelidate/core';

export default {
  setup() {
    return { v$: useVuelidate() };
  },
  data() {
    return {
      isModalVisible: false,
      formData: {
        title: '',
        releaseYear: null,
      },
    };
  },
  validations() {
    return {
      formData: {
        title: {
          required,
          maxLength: maxLength(200),
        },
        releaseYear: { required, between: between(1900, 2200) },
      },
    };
  },
  methods: {
    showModal() {
      this.isModalVisible = true;
    },
    closeModal() {
      this.isModalVisible = false;
      this.formData = {
        title: '',
        releaseYear: null,
      };
    },
    handleSubmit() {

      if (this.v$.formData.$invalid) {
        const errorIndices = Object.keys(this.v$.formData.$errors || {});
        console.log(this.v$.formData.$errors[0]);
        const errorMessage = errorIndices
            .map((index) => {
            const fieldErrors = this.v$.formData.$errors[index];
            const betweenError = fieldErrors?.$validator === 'between' ? 'Value should be between 1900 and 2200.' : '';

            return [betweenError].filter(Boolean).join(' ');
            })
            .filter((message) => message !== '')
            .join('\n');

        alert(`Form contains errors:\n${errorMessage}`);
        return;
}

      this.$emit('submit', this.formData);
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
