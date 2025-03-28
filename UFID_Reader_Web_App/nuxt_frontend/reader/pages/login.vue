<template>
    <div class="login-page">
        <div class="login-container">
            <div class="login-header">
                <img src="~/assets/UF_Monogram.png" alt="Logo" class="login-logo" />
                <h1>Login with Gatorlink</h1>
            </div>
            <form @submit.prevent="handleLogin">
                <div class="form-group">
                    <label for="email">Email</label>
                    <input
                        type="email"
                        id="email"
                        v-model="email"
                        placeholder="Enter your email"
                        required
                    />
                </div>
                <div class="form-group">
                    <label for="password">Password</label>
                    <input
                        type="password"
                        id="password"
                        v-model="password"
                        placeholder="Enter your password"
                        required
                    />
                </div>
                <button type="submit" class="login-button">Login</button>
            </form>
        </div>
    </div>
</template>

<script>
export default {
    data() {
        return {
            email: '',
            password: '',
        };
    },
    methods: {
        handleLogin() {
            // Add your login logic here
            if (this.email && this.password) {
                console.log('Logging in with:', this.email, this.password);
                // Example: Call an API or perform validation
            } else {
                alert('Please fill in all fields.');
            }
        },
    },
};
</script>

<style scoped>
.login-page {
    display: flex;
    justify-content: center;
    align-items: center;
    height: 100vh;
    background-color: #f5f5f5;
}

.login-container {
    background: white;
    padding: 2rem;
    border-radius: 8px;
    box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
    width: 100%;
    max-width: 400px;
}

.login-header {
    text-align: center;
    margin-bottom: 2.5rem;
}

.login-logo {
    display: block;
    margin: 0 auto 1rem;
    max-width: 80px; /* Adjust the maximum width */
    height: auto; /* Maintain aspect ratio */
}

h1 {
    margin-bottom: 0;
    font-size: 1.5rem; /* Adjust the size as needed */
    font-weight: bold; /* Optional: Make the text bold */
    text-align: center; /* Optional: Center the text */
}

.form-group {
    margin-bottom: 1rem;
}

label {
    display: block;
    margin-bottom: 0.5rem;
    font-weight: bold;
}

input {
    width: 100%;
    padding: 0.5rem;
    border: 1px solid #ccc;
    border-radius: 4px;
}

.login-button {
    width: 100%;
    padding: 0.75rem;
    background-color: #007bff;
    color: white;
    border: none;
    border-radius: 4px;
    font-size: 1rem;
    cursor: pointer;
}

.login-button:hover {
    background-color: #0056b3;
}
</style>