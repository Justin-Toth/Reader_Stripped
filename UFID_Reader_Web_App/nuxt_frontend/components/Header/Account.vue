<script setup lang="ts">
    import { Button } from '@/components/ui/button'
    import { DropdownMenu, DropdownMenuContent, DropdownMenuItem, DropdownMenuLabel, DropdownMenuSeparator, DropdownMenuTrigger } from '@/components/ui/dropdown-menu'
</script>

<template>
    <DropdownMenu>
        <DropdownMenuTrigger as-child>
            <Button variant="secondary" size="icon" class="rounded-full">
                <Icon name="lucide:circle-user" class="h-5 w-5" />
                <span class="sr-only">Toggle user menu</span>
            </Button>
        </DropdownMenuTrigger>
        <DropdownMenuContent align="end">
            <!--Section 1-->
            <DropdownMenuLabel>
                <NuxtLink to="/#">
                    My Account
                </NuxtLink>
            </DropdownMenuLabel>
            
            <!--Section 2--> 
            <DropdownMenuSeparator />
            <DropdownMenuItem>
                <NuxtLink to="/#">
                    Settings
                </NuxtLink>
            </DropdownMenuItem>
            <DropdownMenuItem>
                <NuxtLink to="/#">
                    Support
                </NuxtLink>
            </DropdownMenuItem>
           
            <!--Section 3-->
            <DropdownMenuSeparator />
            <DropdownMenuItem>
                <NuxtLink to="/login">
                    Logout
                </NuxtLink>
            </DropdownMenuItem>
        </DropdownMenuContent>
    </DropdownMenu>
</template>