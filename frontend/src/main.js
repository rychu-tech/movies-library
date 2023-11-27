import { createApp } from "vue";
import App from "./App.vue";
import router from "./router";
import "bootstrap/dist/css/bootstrap.css";
import "bootstrap/dist/css/bootstrap.min.css";
import "bootstrap-vue/dist/bootstrap-vue.css";
import "bootstrap/dist/js/bootstrap.bundle.min.js";
import "bootstrap-icons/font/bootstrap-icons.css";
import "bootstrap";

// const BACKEND_URL = 'http://localhost:8080/api';
const app = createApp(App);
// app.config.globalProperties.BACKEND_URL = BACKEND_URL;

app.use(router);

app.mount("#app");
