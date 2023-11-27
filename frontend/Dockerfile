# Use an official Node.js runtime as a parent image
FROM node:14-alpine

# Set the working directory to /app
WORKDIR /app

# Copy package.json and package-lock.json to the working directory
COPY package*.json ./

# Install dependencies
RUN npm install

# Copy the rest of the application code to the working directory
COPY . .

# Build the Vue.js app for production
RUN npm run build

# Expose port 80 to the outside world
EXPOSE 5173

# Command to run the application
CMD ["npm", "run", "dev"]
