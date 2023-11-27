export function handleFormValidation(v$) {
  if (v$.formData.$invalid) {
    const errorIndices = Object.keys(v$.formData.$errors || {});
    const errorMessage = errorIndices
      .map((index) => {
        const fieldErrors = v$.formData.$errors[index];
        const betweenError =
          fieldErrors?.$validator === "between"
            ? "Value should be between 1900 and 2200."
            : "";

        return [betweenError].filter(Boolean).join(" ");
      })
      .filter((message) => message !== "")
      .join("\n");

    alert(`Form contains errors:\n${errorMessage}`);
    return true;
  }
  return false;
}
