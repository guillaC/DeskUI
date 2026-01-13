window.themeBuilder = {
    getCssVariables(selector) {
        const el = document.querySelector(selector)
        if (!el) return {}

        const styles = getComputedStyle(el)
        const result = {}

        for (let i = 0; i < styles.length; i++) {
            const name = styles[i]
            if (name.startsWith("--")) {
                result[name] = styles.getPropertyValue(name).trim()
            }
        }

        return result
    }
}
