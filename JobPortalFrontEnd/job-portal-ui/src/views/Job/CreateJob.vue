<script setup lang="ts">

import {
    onMounted,
    reactive,
    ref
} from "vue";

import Sidebar from "@/components/common/SideBar.vue";
import logo from "@/assets/JobPortal_logo.png";

import { CreateJob } from "@/composables/Job/UseCreateJob";

import {
    GetAllEmploymentType
} from "@/composables/Common/UseGetAllEmploymentType";

import {
    GetAllExperienceLevel
} from "@/composables/Common/UseGetAllExperienceLevel";

import type {
    JobRequest
} from "@/types/Job";

import type {
    EmploymentType
} from "@/types/EmploymentType";

import type {
    ExperienceLevel
} from "@/types/ExperienceLevel";
import { useEditor, EditorContent } from "@tiptap/vue-3";
import StarterKit from "@tiptap/starter-kit";
import Underline from "@tiptap/extension-underline";


const loading = ref(false);

const loadingEmploymentTypes =
    ref(false);

const loadingExperienceLevels =
    ref(false);

const error = ref("");


const employmentTypes =
    ref<EmploymentType[]>([]);

const experienceLevels =
    ref<ExperienceLevel[]>([]);


/*
|--------------------------------------------------------------------------
| Recruiter Sidebar
|--------------------------------------------------------------------------
*/

const recruiterMenu = [

    { name: "Dashboard", icon: "🏠", path: "/recruiterDashboard" },
    { name: "Company Profile", icon: "🏢", path: "/company" },
    { name: "Post Job", icon: "📢", path: "/recruiter/jobs/create" },
    { name: "Manage Jobs", icon: "💼", path: "/recruiter/jobs" },
    { name: "Candidates", icon: "👥", path: "/candidates" },
    { name: "Interviews", icon: "📅", path: "/interview" },
    { name: "Shortlisted", icon: "⭐", path: "shortlisted" }

];


const bottomMenu = [

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


/*
|--------------------------------------------------------------------------
| Form
|--------------------------------------------------------------------------
*/

const form = reactive<JobRequest>({

    title: "",

    description: "",

    location: "",

    refEmploymentType:
        undefined,

    refExperienceLevel:
        undefined,

    minSalary:
        undefined,

    maxSalary:
        undefined,

    deadline:
        undefined

});

/* editor*/
const editor = useEditor({
    content: form.description,

    extensions: [
        StarterKit,
        Underline,
    ],

    onUpdate: ({ editor }) => {
        form.description = editor.getHTML();
    },
});
/*
|--------------------------------------------------------------------------
| Load Employment Types
|--------------------------------------------------------------------------
*/

const loadEmploymentTypes = async () => {

    try {

        loadingEmploymentTypes.value =
            true;

        const response =
            await GetAllEmploymentType();

        employmentTypes.value =
            response.data ?? [];

    } catch (err) {

        console.error(
            "Error loading employment types:",
            err
        );

        error.value =
            "Failed to load employment types";

    } finally {

        loadingEmploymentTypes.value =
            false;

    }

};


/*
|--------------------------------------------------------------------------
| Load Experience Levels
|--------------------------------------------------------------------------
*/

const loadExperienceLevels = async () => {

    try {

        loadingExperienceLevels.value =
            true;

        const response =
            await GetAllExperienceLevel();

        experienceLevels.value =
            response.data ?? [];

    } catch (err) {

        console.error(
            "Error loading experience levels:",
            err
        );

        error.value =
            "Failed to load experience levels";

    } finally {

        loadingExperienceLevels.value =
            false;

    }

};


/*
|--------------------------------------------------------------------------
| Submit
|--------------------------------------------------------------------------
*/

const submitJob = async () => {

    try {

        loading.value = true;

        error.value = "";


        const payload: JobRequest = {

            title:
                form.title.trim(),

            description:
                form.description.trim(),

            location:
                form.location?.trim()
                || undefined,

            refEmploymentType:
                form.refEmploymentType
                || undefined,

            refExperienceLevel:
                form.refExperienceLevel
                || undefined,

            minSalary:
                form.minSalary,

            maxSalary:
                form.maxSalary,

            deadline:
                form.deadline
                || undefined

        };


        const response =
            await CreateJob(payload);


        console.log(
            "Job created:",
            response.data
        );


        alert(
            "Job created successfully"
        );


        // Reset form

        form.title = "";

        form.description = "";

        form.location = "";

        form.refEmploymentType =
            undefined;

        form.refExperienceLevel =
            undefined;

        form.minSalary =
            undefined;

        form.maxSalary =
            undefined;

        form.deadline =
            undefined;


    } catch (err: any) {

        console.error(
            "Error creating job:",
            err
        );

        error.value =
            err.response?.data?.message ||
            "Failed to create job";

    } finally {

        loading.value = false;

    }

};


/*
|--------------------------------------------------------------------------
| Load dropdowns
|--------------------------------------------------------------------------
*/

onMounted(() => {

    loadEmploymentTypes();

    loadExperienceLevels();

});

</script>


<template>

    <div class="dashboard">

        <!-- Sidebar -->

        <Sidebar companyName="Jobsy" slogan="Find Your Sea" :logo="logo" :menuItems="recruiterMenu"
            :bottomMenu="bottomMenu" />


        <!-- Main -->

        <section class="main">

            <div class="page-content">

                <div class="page-header">

                    <div>

                        <h1>
                            Create Job
                        </h1>

                        <p>
                            Create a new job posting
                            for your company.
                        </p>

                    </div>

                </div>


                <!-- Error -->

                <div v-if="error" class="error-message">
                    {{ error }}
                </div>


                <!-- Form Card -->

                <div class="form-card">

                    <form @submit.prevent="submitJob">

                        <!-- Job Title -->

                        <div class="form-group">

                            <label>
                                Job Title
                            </label>

                            <input v-model="form.title" type="text" placeholder="e.g. Software Developer" required />

                        </div>


                        <!-- Description -->

                        <div class="form-group">
                            <label>Description</label>

                            <div class="editor-wrapper">

                                <!-- Toolbar -->
                                <div class="editor-toolbar">

                                    <button type="button" @click="editor?.chain().focus().toggleBold().run()"
                                        :class="{ active: editor?.isActive('bold') }" title="Bold">
                                        <strong>B</strong>
                                    </button>

                                    <button type="button" @click="editor?.chain().focus().toggleItalic().run()"
                                        :class="{ active: editor?.isActive('italic') }" title="Italic">
                                        <em>I</em>
                                    </button>

                                    <button type="button" @click="editor?.chain().focus().toggleUnderline().run()"
                                        :class="{ active: editor?.isActive('underline') }" title="Underline">
                                        <u>U</u>
                                    </button>

                                    <span class="toolbar-divider"></span>

                                    <button type="button"
                                        @click="editor?.chain().focus().toggleHeading({ level: 2 }).run()"
                                        :class="{ active: editor?.isActive('heading', { level: 2 }) }" title="Heading">
                                        H2
                                    </button>

                                    <button type="button" @click="editor?.chain().focus().toggleBulletList().run()"
                                        :class="{ active: editor?.isActive('bulletList') }" title="Bullet List">
                                        •
                                    </button>

                                    <button type="button" @click="editor?.chain().focus().toggleOrderedList().run()"
                                        :class="{ active: editor?.isActive('orderedList') }" title="Numbered List">
                                        1.
                                    </button>

                                    <span class="toolbar-divider"></span>

                                    <button type="button" @click="editor?.chain().focus().undo().run()" title="Undo">
                                        ↶
                                    </button>

                                    <button type="button" @click="editor?.chain().focus().redo().run()" title="Redo">
                                        ↷
                                    </button>

                                </div>

                                <!-- Tiptap editor -->
                                <EditorContent :editor="editor" class="description-editor" />

                            </div>
                        </div>


                        <!-- Location -->

                        <div class="form-group">

                            <label>
                                Location
                            </label>

                            <input v-model="form.location" type="text" placeholder="e.g. Gurugram, Haryana" />

                        </div>


                        <!-- Two Column Row -->

                        <div class="form-row">


                            <!-- Employment -->

                            <div class="form-group">

                                <label>
                                    Employment Type
                                </label>

                                <select v-model="form.refEmploymentType
                                    " :disabled="loadingEmploymentTypes
                                        " required>

                                    <option :value="undefined" disabled>
                                        {{
                                            loadingEmploymentTypes
                                                ? "Loading..."
                                                : "Select Employment Type"
                                        }}
                                    </option>


                                    <option v-for="
type in employmentTypes
                                        " :key="type.id" :value="type.id">
                                        {{
                                            type.description
                                        }}
                                    </option>

                                </select>

                            </div>


                            <!-- Experience -->

                            <div class="form-group">

                                <label>
                                    Experience Level
                                </label>

                                <select v-model="form.refExperienceLevel
                                    " :disabled="loadingExperienceLevels
                                        " required>

                                    <option :value="undefined" disabled>
                                        {{
                                            loadingExperienceLevels
                                                ? "Loading..."
                                                : "Select Experience Level"
                                        }}
                                    </option>


                                    <option v-for="
level in experienceLevels
                                        " :key="level.id" :value="level.id">
                                        {{
                                            level.description
                                        }}
                                    </option>

                                </select>

                            </div>

                        </div>


                        <!-- Salary Row -->

                        <div class="form-row">


                            <!-- Minimum -->

                            <div class="form-group">

                                <label>
                                    Minimum Salary
                                </label>

                                <input v-model.number="form.minSalary
                                    " type="number" min="0" placeholder="e.g. 500000" />

                            </div>


                            <!-- Maximum -->

                            <div class="form-group">

                                <label>
                                    Maximum Salary
                                </label>

                                <input v-model.number="form.maxSalary
                                    " type="number" min="0" placeholder="e.g. 1000000" />

                            </div>

                        </div>


                        <!-- Deadline -->

                        <div class="form-group">

                            <label>
                                Application Deadline
                            </label>

                            <input v-model="form.deadline
                                " type="date" />

                        </div>


                        <!-- Actions -->

                        <div class="form-actions">

                            <button type="submit" class="submit-btn" :disabled="loading ||
                                loadingEmploymentTypes ||
                                loadingExperienceLevels
                                ">

                                {{
                                    loading
                                        ? "Creating..."
                                        : "Create Job"
                                }}

                            </button>

                        </div>

                    </form>

                </div>

            </div>

        </section>

    </div>

</template>


<style scoped>
.dashboard {

    display: flex;

    height: 100vh;

}


.main {

    flex: 1;

    background: #f5f7fb;

    height: 100vh;

    overflow-y: auto;

}


.page-content {

    padding: 30px;

    max-width: 1100px;

    margin: 0 auto;

}


.page-header {

    margin-bottom: 25px;

}


.page-header h1 {

    margin: 0;

    font-size: 28px;

    font-weight: 700;

    color: #1f2937;

}


.page-header p {

    margin-top: 6px;

    color: #6b7280;

    font-size: 14px;

}


.form-card {

    background: white;

    border-radius: 12px;

    padding: 30px;

    box-shadow:
        0 2px 10px rgba(0, 0, 0, 0.05);

}


.form-group {

    display: flex;

    flex-direction: column;

    margin-bottom: 20px;

}


.form-group label {

    font-size: 14px;

    font-weight: 600;

    color: #374151;

    margin-bottom: 8px;

}


.form-group input,
.form-group textarea,
.form-group select {

    width: 100%;

    box-sizing: border-box;

    padding: 11px 13px;

    border: 1px solid #d1d5db;

    border-radius: 8px;

    font-size: 14px;

    background: white;

    color: #1f2937;

    outline: none;

    transition:
        border-color 0.2s,
        box-shadow 0.2s;

}


.form-group textarea {

    resize: vertical;

    min-height: 130px;

}


.form-group input:focus,
.form-group textarea:focus,
.form-group select:focus {

    border-color: #6366f1;

    box-shadow:
        0 0 0 3px rgba(99, 102, 241, 0.1);

}


.form-group select {

    cursor: pointer;

}


.form-row {

    display: grid;

    grid-template-columns:
        1fr 1fr;

    gap: 20px;

}


.form-actions {

    display: flex;

    justify-content: flex-end;

    margin-top: 10px;

}


.submit-btn {

    border: none;

    border-radius: 8px;

    padding: 12px 24px;

    background: #4f46e5;

    color: white;

    font-size: 14px;

    font-weight: 600;

    cursor: pointer;

    transition:
        background 0.2s,
        transform 0.1s;

}


.submit-btn:hover {

    background: #4338ca;

}


.submit-btn:active {

    transform: translateY(1px);

}


.submit-btn:disabled {

    opacity: 0.6;

    cursor: not-allowed;

}


.error-message {

    background: #fee2e2;

    color: #b91c1c;

    padding: 12px 15px;

    border-radius: 8px;

    margin-bottom: 20px;

    font-size: 14px;

}


@media (max-width: 700px) {

    .page-content {

        padding: 20px;

    }

    .form-card {

        padding: 20px;

    }

    .form-row {

        grid-template-columns: 1fr;

        gap: 0;

    }

}

.editor-wrapper {
    width: 100%;
}

.editor-toolbar {
    display: flex;
    align-items: center;
    gap: 6px;
    padding: 8px;
    border: 1px solid #d1d5db;
    border-bottom: none;
    border-radius: 8px 8px 0 0;
    background: #f8f9fc;
}

.editor-toolbar button {
    min-width: 34px;
    height: 32px;
    padding: 0 8px;
    border: 1px solid #d1d5db;
    background: white;
    border-radius: 5px;
    cursor: pointer;
    font-size: 14px;
}

.editor-toolbar button:hover {
    background: #f3f4f6;
}

.editor-toolbar button.active {
    background: #4f46e5;
    color: white;
    border-color: #4f46e5;
}

.toolbar-divider {
    width: 1px;
    height: 24px;
    background: #d1d5db;
    margin: 0 4px;
}

/* Tiptap content area */
.description-editor {
    min-height: 180px;
    border: 1px solid #d1d5db;
    border-radius: 0 0 8px 8px;
    background: white;
}

/* Important: actual ProseMirror editor */
.description-editor :deep(.ProseMirror) {
    min-height: 180px;
    padding: 12px 13px;
    outline: none;
    font-size: 14px;
    line-height: 1.6;
    color: #1f2937;
}

/* Placeholder-like empty editor spacing */
.description-editor :deep(.ProseMirror p) {
    margin: 0 0 8px;
}

/* Lists */
.description-editor :deep(.ProseMirror ul) {
    padding-left: 25px;
}

.description-editor :deep(.ProseMirror ol) {
    padding-left: 25px;
}

/* Headings */
.description-editor :deep(.ProseMirror h2) {
    font-size: 20px;
    font-weight: 700;
    margin: 15px 0 8px;
}

.description-editor :deep(.ProseMirror h3) {
    font-size: 17px;
    font-weight: 700;
    margin: 12px 0 8px;
}
</style>