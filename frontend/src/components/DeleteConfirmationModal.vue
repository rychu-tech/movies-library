<template>
  <div>
    <button @click="showModal" class="btn btn-danger">
      <i class="bi bi-trash"></i> Delete
    </button>
    <div v-if="isModalVisible" class="custom-modal" @click.self="closeModal">
      <div class="modal-content" @click.stop>
        <div class="modal-header">
          <h5 class="modal-title">Delete Confirmation</h5>
          <button type="button" class="btn-close" @click="closeModal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
          <p>Do you want to delete this item?</p>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-secondary" @click="closeModal">No</button>
          <button type="button" class="btn btn-danger" @click="confirmDelete">Yes</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    movieId: {
      type: Number,
      required: true,
    },
  },
  data() {
    return {
      isModalVisible: false,
    };
  },
  methods: {
    showModal() {
      this.isModalVisible = true;
    },
    confirmDelete() {
      this.$emit('submitDelete', this.movieId);
      this.closeModal();
    },
    closeModal() {
      this.isModalVisible = false;
    },
  },
};
</script>

<style scoped>
.custom-modal {
  display: flex;
  align-items: center;
  justify-content: center;
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

.modal-footer {
    align-items: center;
    flex-direction: row;
    justify-content: space-between;
    display: flex;
}
</style>
