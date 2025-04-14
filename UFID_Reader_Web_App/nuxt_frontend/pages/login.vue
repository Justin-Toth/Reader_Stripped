<script setup lang="ts">
import { EyeIcon } from 'lucide-vue-next'
import { toTypedSchema } from '@vee-validate/zod'
import { z } from 'zod'

definePageMeta({
  layout: 'no-auth'
})

const router = useRouter()

// Validation schema
const loginSchema =    toTypedSchema(z.object({
  email: z.string().email(),
  password: z.string().min(6, 'Password must be at least 6 characters'),
}))

const showPassword = ref(false)

// This will need to be replaced with the actual login function
// This is just a placeholder for demonstration purposes
function handleLogin(values: { email: string; password: string }) {
  console.log('Email:', values.email)
  console.log('Password:', values.password)

  // Simulate a successful login
  router.push('/')
}
</script>


<template>
    <div class="flex items-center justify-center min-h-screen px-4 relative">
      <!-- Mode Toggle -->
      <div class="absolute top-4 right-4">
        <HeaderModeToggle />
      </div>
  
      <!-- Card Container -->
      <div class="flex w-full max-w-5xl bg-white dark:bg-gray-800 shadow-xl rounded-2xl overflow-hidden min-h-[600px]">
        <!-- Left: Form -->
        <div class="w-1/2 p-10 flex flex-col">
          <div class="mb-4">
            <img src="@/assets/UF_Monogram.png" alt="Logo" class="w-16 h-11" >
          </div>
  
          <div class="flex flex-col justify-center flex-1">
            <div class="w-full max-w-md">
              <h1 class="text-3xl font-bold mb-6">Login to Your Account</h1>
              <div class="h-px bg-gray-300 dark:bg-gray-600 mb-6" />
  
              <!-- ShadCN Form -->
              <Form :validation-schema="loginSchema" class="space-y-4" @submit="handleLogin">
                <!-- Email -->
                <FormField v-slot="{ componentField }" name="email">
                  <FormItem>
                    <FormLabel>Email</FormLabel>
                    <FormControl>
                      <Input v-bind="componentField" type="email" placeholder="Email" />
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                </FormField>
  
                <!-- Password -->
                <FormField v-slot="{ componentField }" name="password">
                  <FormItem>
                    <FormLabel>Password</FormLabel>
                    <FormControl>
                      <div class="relative">
                        <Input
                          v-bind="componentField"
                          :type="showPassword ? 'text' : 'password'"
                          placeholder="Password"
                        />
                        <button type="button" class="absolute right-3 top-2.5" @click="showPassword = !showPassword">
                          <EyeIcon class="w-5 h-5" />
                        </button>
                      </div>
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                </FormField>
  
                <Button type="submit" class="w-full">
                    Sign In
                </Button>
              </Form>
            </div>
          </div>
        </div>
  
        <!-- Right Panel -->
        <div class="w-1/2 bg-gradient-to-br from-[#FA4616] to-[#0021A5] text-white p-10 flex flex-col justify-center items-center">
          <h2 class="text-3xl font-bold mb-4">Don't Have an Account Yet?</h2>
          <p class="mb-6 text-center max-w-xs">Sign up to gain access to all your courses!</p>
          <Button class="bg-white text-black hover:bg-gray-100">Sign Up</Button>
        </div>
      </div>
    </div>
  </template>