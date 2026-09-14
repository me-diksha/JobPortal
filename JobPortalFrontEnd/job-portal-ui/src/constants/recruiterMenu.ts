import type { MenuItem } from "@/types/MenuItem";
export const recruiterMenu = [
    { name: "Dashboard", icon: "🏠", path: "/recruiterDashboard" },
    { name: "Company Profile", icon: "🏢", path: "/company" },
    { name: "Post Job", icon: "📢", path: "/recruiter/jobs/create" },
    { name: "Manage Jobs", icon: "💼", path: "/recruiter/jobs" },
    { name: "Candidates", icon: "👥", path: "/candidates" },
    { name: "Interviews", icon: "📅", path: "/interview" },
    { name: "Shortlisted", icon: "⭐", path: "shortlisted" }
];
export const bottomMenu = [
    { name: "Preferences", icon: "⚙" },
    { name: "Dark Mode", icon: "🌙" },
    { name: "Help Center", icon: "💬" }
];