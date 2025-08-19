Local Git hooks for build and tests

These hooks block commits and pushes when the project doesn't build or when unit/integration tests fail.

Enable once per clone:

1. Point git to this hooks folder
   git config core.hooksPath .githooks

2. Make scripts executable (Linux/macOS/WSL/Git Bash)
   chmod +x .githooks/pre-commit .githooks/pre-push

Notes

- Hooks run locally only; users can bypass with --no-verify. Use CI + branch protection to enforce on the server.
- pre-commit: builds and runs unit/integration tests quickly.
- pre-push: restores, builds, and runs unit/integration tests again.
