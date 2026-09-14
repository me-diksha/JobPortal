import type { MenuItem } from "@/types/MenuItem";
export const candidateMenu: MenuItem[] = [
    {
        name: "Home",
        icon: "🏠",
        path: "/candidateDashboard"
    },
    {
        name: "Browse Jobs",
        icon: "💼",
        path: "/loadjobs"
    },
    {
        name: "Applications",
        icon: "📄",
        path: "/applications"
    },
    {
        name: "Saved Jobs",
        icon: "⭐",
        path: "/savedJobs"
    }
];

export const candidateBottomMenu = [
    {
        name: "Preferences",
        icon: "⚙"
    },
    {
        name: "Dark Mode",
        icon: "🌙"
    },
    {
        name: "Help Center",
        icon: "💬"
    }
];