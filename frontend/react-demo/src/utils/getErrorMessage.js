const getErrorMessage = (error) => {
  const resMessage =
    (error.response && error.response.data && error.response.data.message) ||
    error.message ||
    error.toString();
  return `Error = ${resMessage}`;
};

export default getErrorMessage;
