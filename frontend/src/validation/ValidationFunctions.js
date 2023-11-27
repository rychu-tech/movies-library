import { required, maxLength, between } from "@vuelidate/validators";

export const movieValidationRules = {
  title: {
    required,
    maxLength: maxLength(200),
  },
  releaseYear: {
    required,
    between: between(1900, 2200),
  },
};
